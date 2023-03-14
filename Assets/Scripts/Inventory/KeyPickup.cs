using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    
    
    public void PickupInteraction(CharacterController controller)
    {
        //detect if not the player collided 
        if(!controller.TryGetComponent(out InventoryManager inventoryManager))
        {
            return;
        }
        
        // add 1 to the player inventory 
        inventoryManager.IncreaseCurrentItemAmount(itemType);
        // Destroy on pickup
        Destroy(gameObject);
    }
}
