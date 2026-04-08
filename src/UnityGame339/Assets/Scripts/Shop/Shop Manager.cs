using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// testing pushing #3
public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4]; // aray of shop items - [columns, rows]
    public float coins; // currency 
    public Text CointsText;
    public FadeAwayText FadeAwayText;
    
    void Start()
    {
        CointsText.text = "$" + coins.ToString();
        
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

    
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Item").GetComponent<EventSystem>().currentSelectedGameObject;

        // Check if we have enough coins to purchase the item:
        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            // subtract price from our coins
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            
            // increase item quantity
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++; 
            
            // update coins after purchase
            CointsText.text = "Coins:" + coins.ToString(); 
            
            // update item quantity
            ButtonRef.GetComponent<ButtonInfo>().QuantityText.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
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
