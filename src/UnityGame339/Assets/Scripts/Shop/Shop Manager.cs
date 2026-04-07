using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// testing pushing #3
public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4]; // aray of shop items - [columns, rows]
    public float coins; // currency 
    public Text CoinsText;
    public FadeAwayText FadeAwayText;
    
    private InventoryManager inventoryManager;
    
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        
        CoinsText.text = "$" + coins.ToString();
        
        // IDs
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        
        // Prices
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        
        // Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
    } 

    
    public void Buy(GameObject buttonObject)
    {
        var info = buttonObject.GetComponent<Item>();

        int itemID = info.ItemID;
        
        // Check if we have enough coins to purchase the item:
        if (coins >= shopItems[2, itemID])
        {
            // subtract price from our coins
            coins -= shopItems[2, itemID];
            
            // increase item quantity
            shopItems[3, itemID]++; 
            
            // update coins after purchase
            CoinsText.text = "$" + coins.ToString(); 
            
            // update item quantity
            info.QuantityText.text = shopItems[3, itemID].ToString();
            
            info.SendToInventory();
        }
        else
        {
            CantBuy();
        }
    }

    public void CantBuy()
    {
        FadeAwayText.MakeTextVisible();
    }
}
