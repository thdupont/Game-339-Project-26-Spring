using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public Text QuantityText;
    public GameObject ShopManagerObj;
    
    void Update()
    {
        PriceText.text = "Price: $" + ShopManagerObj.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        
        QuantityText.text = ShopManagerObj.GetComponent<ShopManager>().shopItems[3, ItemID].ToString();
    }
}
