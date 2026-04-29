using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// testing pushing #3
public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4]; // aray of shop items - [columns, rows]
    public int coins; // currency 
    public Text CoinsText;
    public FadeAwayText FadeAwayText;
    public Item[] shopItemButtons;
    
    private InventoryManager inventoryManager;
    private MoneyManager moneyManager;
    
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        
        // IDs
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        
        // Prices
        shopItems[2, 1] = 8;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 20;
        
        // Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
    } 

    
    public void Buy()
    {
        Item info = GetButton();
        if (info == null) return;

        int itemID = info.ItemID;
        
        // Check if we have enough coins to purchase the item:
        if (coins >= shopItems[2, itemID])
        {
            // subtract price from our coins
            coins -= shopItems[2, itemID];
            
            
            // increase item quantity
            shopItems[3, itemID] += 1; 
            
            // update coins after purchase
            CoinsText.text = "$" + coins.ToString(); 
            
            // update item quantity
            info.QuantityText.text = shopItems[3, itemID].ToString();
            
            info.SendToInventory(1);
        }
        else
        {
            CantBuy();
        }
    }
    
    public void DecreaseItemQuantity(int itemID)
    {
        //Debug.Log("DecreaseItemQuantity called with itemID: " + itemID);
        //Debug.Log("Current shopItems[3, " + itemID + "] = " + shopItems[3, itemID]);
        
        if (shopItems[3, itemID] > 0)
        {
            shopItems[3, itemID] -= 1;
        }
        else
        {
            //Debug.Log("Quantity was already 0, not decreasing.");
        }
    }

    public void CantBuy()
    {
        FadeAwayText.MakeTextVisible();
    }

    public Item GetButton()
    {
        GameObject buttonObject = EventSystem.current.currentSelectedGameObject;

        if (buttonObject == null)
        {
            return null; 
        }
    
        Item info = buttonObject.GetComponent<Item>();

        if (info == null)
        {
            return null; 
        }

        return info;
    }
}
