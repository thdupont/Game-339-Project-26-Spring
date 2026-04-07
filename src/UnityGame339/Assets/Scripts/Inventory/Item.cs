using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // pulled form old ButtonInfo script
    public int ItemID;
    public Text PriceText;
    public Text QuantityText;
    public Text NameText;
    public GameObject ShopManagerObj;
    
    public string itemName;
    public int quantity;
    public int price;
    public Sprite sprite;
    
    private InventoryManager inventoryManager;
  
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    void Update()
    {
        PriceText.text = "Price: $" + ShopManagerObj.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        QuantityText.text = ShopManagerObj.GetComponent<ShopManager>().shopItems[3, ItemID].ToString();
    }

    public void SendToInventory()
    {
        inventoryManager.AddItem(itemName, quantity, sprite, price);
    }
}
