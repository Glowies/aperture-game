using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    // Debug booleans
    public bool PickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onPlantTriggerPickUp += OnPlantPickUp;
        GameEvents.instance.onPlantTriggerThrow += OnPlantThrow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlantPickUp()
    {
        // event goes here
        PickedUp = true;
        Debug.Log("pick up plant event");
    }
    private void OnPlantThrow()
    {
        // event goes here
        PickedUp = false;
        Debug.Log("throw plant event");
    }
}
