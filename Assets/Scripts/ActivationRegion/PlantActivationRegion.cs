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
