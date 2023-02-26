using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    private void OnTriggerEnter(Collider other) {
        //detect if not the player collided 
        if(!other.TryGetComponent(out InventoryManager inventoryManager))
        {
            return;
        }
        //else - if the player collided with the object 
        Debug.Log("Player Collision");
        // add 1 to the player inventory 
        inventoryManager.IncreaseCurrentItemAmount(itemType);
        // Destroy on pickup
        Destroy(gameObject);  
    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
