using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRegion : MonoBehaviour
{
    //These fields are the item type and how much items this gameObject consumes
    // upon interaction 
    [SerializeField] ItemType itemType;
    [SerializeField] int consume = 2;

    public void OnInteract(CharacterController controller)
    {
        if(!controller.TryGetComponent(out InventoryManager inventory))
        {
            return;
        }
        //get the player inventory and reduce the amount of itemType by consume if the player 
        // inventory has enough of that item
        if (inventory.GetCurrentItemAmount(itemType) >= consume){
            inventory.ReduceCurrentItemAmount(itemType, consume);
            gameObject.SetActive(false);
        }
        else{
            Debug.Log("You dont have enough of this item");
        }
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
