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
    
    //===INFO===
    [SerializeField]
    private string itemName;
    
    [SerializeField]
    private int quantity;
    
    [SerializeField]
    private int price;
    
    [SerializeField]
    private Sprite sprite;
    
    [TextArea]
    [SerializeField]
    private string itemDescription;
    
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

    public void SendToInventory(int currentQuantity)
    {
        inventoryManager.AddItem(itemName, currentQuantity, sprite, price, itemDescription);
    }
}
