using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellSlots : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int itemID;
    public int quantity;
    public Sprite itemSprite;
    public int price;
    public bool isSellable;
    public GameObject selectedShader;
    public bool isSlotSelected;
    
    // ---Sell Slot UI---
    [SerializeField] private Text quantityText;
    [SerializeField] private Image itemImage;
    public Sprite emptySprite;
    
    // ---Description Panel UI---
    public Text itemNameText;
    public Text itemPriceText;
    public Text sellStatusText;
    
    private SellManager sellManager;
    
    void Start()
    {
        sellManager = FindObjectOfType<SellManager>();
        Debug.Log("SellSlot initialized: " + gameObject.name);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SellSlot clicked: " + itemName);
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick();
    }

    public void OnLeftClick()
    {
        if (quantity <= 0) return;

        sellManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        isSlotSelected = true;
        
        itemNameText.text = itemName;

        if (isSellable)
        {
            itemPriceText.text = "Price: $" + price.ToString();
            sellStatusText.text = "Sell this item?";
        }
        else
        {
            itemPriceText.text = "Price: NA";
            sellStatusText.text = "This item cannot be sold.";
        }
    }

    public void FillSlot(string itemName, int itemID, int quantity,
        Sprite itemSprite, int price, bool isSellable)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.price = price;
        this.itemID = itemID;
        this.isSellable = isSellable;
        
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
    }

    public void EmptySlot()
    {
        itemName = "";
        itemID = 0;
        quantity = 0;
        itemSprite = null;
        price = 0;
        isSellable = false;

        itemImage.enabled = false;
        itemImage.sprite = emptySprite;
        quantityText.enabled = false;
        itemNameText.text = "";
        itemPriceText.text = "";
        sellStatusText.text = "";
        selectedShader.SetActive(false);
        isSlotSelected = false;
    }
    
    public void UpdateQuantityText(int quantity)
    {
        quantityText.text = quantity.ToString();
        quantityText.enabled = quantity > 0;
    }
}
