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
        plant.ToggleDeadPlant(false);
        plant.TogglePastPlantMesh(false);

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
        
        plant.ToggleDeadPlant(true);
        plant.TogglePastPlantMesh(true);

        hasPlant = false;
        currentPlant = null;
        OnExit.Invoke();
    }
}
