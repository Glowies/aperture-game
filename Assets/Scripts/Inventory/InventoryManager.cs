using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Make an array of inventory slots
    [SerializeField] InventorySlot[] invSlots;

    // make a class for the inventory slot, make it System.Serializable so we can see it in Inspector
    [System.Serializable]
    private class InventorySlot{
        //Each inventory slot will have two public attributes:
        // 1. The type of item
        // 2. The amount of that item

        public ItemType itemType;
        public int itemAmount;
    }

    private InventorySlot GetInventorySlot(ItemType itemType){
        //loop through inventory slots and if the itemType matches, return the inventory slot 
        foreach(InventorySlot slot in invSlots){
            if (slot.itemType == itemType){
                return slot;
            }
        }
        return null;
    }

    public int GetCurrentItemAmount(ItemType itemType){
        // get the amount of a certain item the player currently has 
        return GetInventorySlot(itemType).itemAmount;
    }

    public int ReduceCurrentItemAmount(ItemType itemType, int reduce){
        // reduce the amount of a certain item the player currently has by reduce if 
        // the amount of the item will remain above 0
        int reducedAmount = GetInventorySlot(itemType).itemAmount - reduce;
        if (reducedAmount >= 0){
            GetInventorySlot(itemType).itemAmount = reducedAmount;
            return reducedAmount;
        } 
        return GetInventorySlot(itemType).itemAmount;
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
