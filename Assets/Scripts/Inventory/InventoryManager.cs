using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        public Sprite itemImage;
        public int itemAmount;
    }

    [SerializeField] TextMeshProUGUI[] keyCountUI;

    [SerializeField] Image[] imageArray;


    private InventorySlot GetInventorySlot(ItemType itemType){
        //loop through inventory slots and if the itemType matches, return the inventory slot 
        foreach(InventorySlot slot in invSlots){
            if (slot.itemType == itemType){
                return slot;
            }
        }
        return null;
    }

    // private void DisplayGrayKeyCount(){
    //     // WHYYYYY
    //     int keyCount = GetCurrentItemAmount(ItemType.GrayKey);
    //     grayKeyCountUI.text = keyCount.ToString();
    // }
    // private void DisplayRedKeyCount(){
    //     // WHYYYYY
    //     int redCount = GetCurrentItemAmount(ItemType.RedKey);
    //     redKeyCountUI.text = redCount.ToString();
    // }

    public int GetCurrentItemAmount(ItemType itemType){
        // get the amount of a certain item the player currently has 
        return GetInventorySlot(itemType).itemAmount;
    }

    public int ReduceCurrentItemAmount(ItemType itemType, int reduce){
        // reduce the amount of a certain item the player currently has by reduce if 
        // the amount of the item will remain above 0
        int reducedAmount = GetInventorySlot(itemType).itemAmount - reduce;
        if (reducedAmount >= 0){
            GetInventorySlot(itemType).itemAmount -= reduce;
            if (reducedAmount == 0){
                imageArray[((int)itemType)].enabled = false;
                keyCountUI[((int)itemType)].enabled = false;
            }
            return reducedAmount;
        } 
        
        keyCountUI[((int)itemType)].text = GetInventorySlot(itemType).itemAmount.ToString(); //update the key count in UI
        return GetInventorySlot(itemType).itemAmount;
    }

    public int IncreaseCurrentItemAmount(ItemType itemType){
        // add the amount of a certain item the player currently has by 1

        // if the amount is 0 and this function is called, enable UI elements
        int increasedAmount = GetInventorySlot(itemType).itemAmount++;
        if (GetInventorySlot(itemType).itemAmount == 1){
            imageArray[((int)itemType)].enabled = true; 
            keyCountUI[((int)itemType)].enabled = true;
        }
        // GetInventorySlot(itemType).itemAmount++;

        keyCountUI[((int)itemType)].text = GetInventorySlot(itemType).itemAmount.ToString(); //update the key count in UI
        return increasedAmount;
    }

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {}
        
    // }
}
