using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantActivationRegion : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    //boolean to check if the region already has a plant
    public bool hasPlant; 

    // the current plant object in the activation region
    public Plant currentPlant;

    //plant audio
    public AudioSource sfx1;
    public AudioSource sfx2;
    public AudioClip sfx_plant_glow_loop;
    public AudioClip sfx_plant_sparkle;


    private void OnTriggerEnter(Collider other)
    {
        // Only register entry if it's a plant
        if(!other.TryGetComponent(out Plant plant))
        {
            return;
        }
        // if plant already in this region do nothing
        if (hasPlant){
            return;
        }
        //set hasPlant to true 
        hasPlant = true;
        // set the currentPlant plant object
        currentPlant = plant;
        // teleport plant that collided to center of prefab by getting position of plant activation region
        Vector3 position = transform.position;
        currentPlant.transform.position = position;
        //Set plant TAv mesh to disabled, enable the dead plant in teh future 
        currentPlant.ToggleDeadPlant(false);
        currentPlant.TogglePastPlantMesh(false);

        OnEnter.Invoke();

        //glow
        sfx1.clip = sfx_plant_glow_loop;
        sfx1.loop = true;
        sfx1.Play();

        //randomized sparkle
        sfx2.clip = sfx_plant_sparkle;
        sfx2.volume = 0.4f;
        sfx2.loop = true;
        sfx2.pitch = Random.Range(-10, 10);
        sfx2.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        // Only register entry if it's a plant
        if(!other.TryGetComponent(out Plant plant))
        {
            return;
        }
        //check if the plant exiting region is the activation region's current plant
        if (plant != currentPlant){
            // if so, don't do anything
            return;
        }
        
        currentPlant.ToggleDeadPlant(true);
        currentPlant.TogglePastPlantMesh(true);

        hasPlant = false;
        currentPlant = null;
        OnExit.Invoke();
    }
}
