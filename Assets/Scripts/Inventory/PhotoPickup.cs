using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPickup : MonoBehaviour
{
    public void PickupInteraction(CharacterController controller)
    {
        //detect if not the player collided 
        if(!controller.TryGetComponent(out InventoryManager inventoryManager))
        {
            return;
        }
        
        // add 1 to the player inventory photo count
        inventoryManager.PhotoPickedUp();
        // Destroy on pickup
        Destroy(gameObject);
    }
}
