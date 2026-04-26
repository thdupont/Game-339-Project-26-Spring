using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    private bool isMenuActivated;
    
    void Update()
    {
        // Open Inventory with "Q"
        if (Input.GetKeyDown(KeyCode.Q) && !isMenuActivated)
        {
            Debug.Log("Pressed Q");
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            isMenuActivated = true;
        }
        
        else if (Input.GetKeyDown(KeyCode.Q) && isMenuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            isMenuActivated = false;
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, 
        int price, string itemDescription)
    {
        Debug.Log("itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + itemSprite + 
                  " price = " + price + " itemDescription = " + itemDescription);
        
        // stack into existing slot with same item
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, price, itemDescription);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, price, itemDescription);
                }
                return leftOverItems;
            }
        }
        // find empty slot
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, price, itemDescription);
                
                if (leftOverItems > 0) 
                { 
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, price, itemDescription);
                } 
                return leftOverItems;
            }
            
        }
        
        return quantity;
    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isItemSelected = false;
        }
    }
}
