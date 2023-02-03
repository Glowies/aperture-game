using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlantTriggerArea : MonoBehaviour
{
    public bool IsPickedUp = false;
    // Check if there is something collided
    public bool triggerStay = false;

    void Start()
    {
        
    }

    void Update()
    {
        // Depending on if picked up or not, do different actions
        if (Input.GetKeyDown(KeyCode.E) && triggerStay && !IsPickedUp)
        {
            IsPickedUp = true;
            GameEvents.instance.PlantTriggerPickUp();
            Debug.Log("E was pressed to pick up plant!");
        }
        if (Input.GetKeyDown(KeyCode.Q) && triggerStay && IsPickedUp)
        {
            IsPickedUp = false;
            GameEvents.instance.PlantTriggerThrow();
            Debug.Log("Q was pressed to throw plant!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // maybe show a text prompt here?
        triggerStay = true;
        Debug.Log("Entering area! Press E to pick up plant, Q to throw plant");
    }
    private void OnTriggerExit(Collider other)
    {
        triggerStay = false;
        Debug.Log("Exiting area!");
    }
}
