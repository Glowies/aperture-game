using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantActivationRegion : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        // Only register entry if it's a plant
        if(!other.TryGetComponent(out Plant plant))
        {
            return;
        }
        
        plant.ToggleDeadPlant(false);

        OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        // Only register entry if it's a plant
        if(!other.TryGetComponent(out Plant plant))
        {
            return;
        }

        plant.ToggleDeadPlant(true);

        OnExit.Invoke();
    }
}
