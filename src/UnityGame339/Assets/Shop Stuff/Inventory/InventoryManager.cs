using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    private bool isMenuActivated;
    private ShopManager shopManager;
    private AudioManager audioManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        audioManager = FindObjectOfType<AudioManager>();
        
        // tells you where to find the debug log file
        LogToFile("Log file location: " + Application.persistentDataPath);
    }
    
    void Update()
    {
        // Open Inventory with "Q"
        if (Keyboard.current.qKey.wasPressedThisFrame && !isMenuActivated)
        {
            //LogToFile("Pressed Q");
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            isMenuActivated = true;
            //LogToFile("Inventory opened");
        }
        
        // close inventory
        else if (Keyboard.current.qKey.wasPressedThisFrame && isMenuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            isMenuActivated = false;
            //LogToFile("Inventory closed");
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, 
        int price, string itemDescription, int itemID)
    {
        //LogToFile("itemName = " + itemName + ", quantity = " + quantity + ", price = " + price);
        
        // stack into existing slot with same item
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, price, itemDescription, itemID);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, price, itemDescription, itemID);
                }
                
                audioManager.PlayAddItemSfx();
                LogToFile("Play AddItemSfx");
                return leftOverItems;
            }
        }
        // find empty slot
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, price, itemDescription, itemID);
                
                if (leftOverItems > 0) 
                { 
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, price, itemDescription, itemID);
                } 
                
                audioManager.PlayAddItemSfx();
                LogToFile("Play AddItemSfx");
                return leftOverItems;
            }
        }
        
        return quantity;
    }

    public void UseItem(string itemName,  int itemID)
    {
        //LogToFile("UseItem called " + itemName + ", itemID: " + itemID);
        for (int i = 0; i < itemSOs.Length; i++)
        {
            //LogToFile("Checking itemSOs " + i + ". itemID: " + itemSOs[i].itemID + " against itemID: " + itemID);
            if (itemSOs[i].itemID == itemID)
            {
                //LogToFile("Item " + itemSOs[i].itemName + " used");
                itemSOs[i].UseItem();
                shopManager.DecreaseItemQuantity(itemID);
                audioManager.PlayUseItemSfx();
                LogToFile("Play UseItemSfx");
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
    
    /// ---DEBUG FILE---
    void LogToFile(string message)
    {
        string path = Application.persistentDataPath + "/gamelog.txt";
        File.AppendAllText(path, System.DateTime.Now + ": " + message + "\n");
        Debug.Log(message);
    }
}
