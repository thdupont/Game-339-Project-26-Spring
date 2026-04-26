using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //===ITEM DATA===
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public int price;
    public string itemDescription;
    public bool isFull;
    public Sprite emptySprite;

    [SerializeField] 
    private int maxNumberOfItems;
    
    //===ITEM SLOT===
    [SerializeField]
    private Text quantityText;
    
    [SerializeField]
    private Image itemImage;
    
    //===ITEM DESCRIPTION SLOT===
    public Image itemDescriptionImage;
    public Text itemDescriptionText;
    public Text itemDescriptionNameText;
    
    //===OTHER===
    private InventoryManager inventoryManager;
    public GameObject selectedShader;
    public bool isItemSelected;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    public int AddItem(string itemName, int quantity, Sprite itemsprite, 
        int price, string itemDescription)
    {
        // check to see if slot is full already
        if (isFull)
        {
            return quantity;
        }
        
        // update name
        this.itemName = itemName;
        
        // update image
        this.itemSprite = itemsprite;
        itemImage.sprite = itemsprite;
        itemImage.enabled = true;
        
        // update price 
        this.price = price;
        
        // update description
        this.itemDescription = itemDescription;
        
        // update quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            this.isFull = true;
            
             // return leftovers
             int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems; 
            return extraItems;
        }
        
        // update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }                                                            
    }

    public void OnLeftClick()
    {
        if (isItemSelected)
        {
            inventoryManager.UseItem(itemName);
            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();
            
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }

        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true); 
            isItemSelected = true;
                    
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        
    }

    public void OnRightClick()
    {
        
    }
    
    private void EmptySlot()
    { 
        quantityText.enabled = false;
        itemImage.enabled = false;
        itemImage.sprite = emptySprite;
        
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }
}
