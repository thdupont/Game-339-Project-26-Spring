using UnityEngine;
using UnityEngine.EventSystems;

public class SellManager : MonoBehaviour
{
    public GameObject SellMenu;
    public SellSlots[] sellSlots;
    
    private ShopManager shopManager;
    private InventoryManager inventoryManager;
    private MoneyManager moneyManager;
    private bool isSellActive;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        isSellActive = false;
    }
    
    public void OnClickSellMenu()
    {
        if (!isSellActive)
        {
            SyncWithInventory();
            SellMenu.SetActive(true);
            isSellActive = true;
            Debug.Log("Sell Menu Active");
        }
        else
        {
            SellMenu.SetActive(false);
            isSellActive = false;
            Debug.Log("Sell Menu Inactive");
        }
    }
    
    // mirror the inventory slots into sell slots
    public void SyncWithInventory()
    {
        for (int i = 0; i < sellSlots.Length; i++)
        {
            ItemSlot slot = inventoryManager.itemSlot[i];

            if (slot.quantity > 0)
            {
                bool isSellable = false;
                foreach (ItemSO item in inventoryManager.itemSOs)
                {
                    if (item.itemID == slot.itemID)
                    {
                        isSellable = item.isSellable;
                        break;
                    }
                }

                sellSlots[i].FillSlot(slot.itemName, slot.itemID, slot.quantity, 
                    slot.itemSprite, slot.price, isSellable);
            }
            else
            {
                sellSlots[i].EmptySlot();
            }
        }
    }
    
    public void Sell()
    {
        SellSlots selectedSlot = null;
        foreach (SellSlots slot in sellSlots)
        {
            if (slot.isSlotSelected)
            {
                selectedSlot = slot;
                break;
            }
        }

        if (selectedSlot == null)
        {
            Debug.Log("No slot selected.");
            return;
        }

        if (!selectedSlot.isSellable)
        {
            Debug.Log("Item cannot be sold.");
            return;
        }
        
        // get player's coins
        moneyManager.coins += selectedSlot.price;

        // remove item from inventory
        inventoryManager.RemoveItem(selectedSlot.itemName, selectedSlot.itemID);

        // update sell slot
        selectedSlot.quantity -= 1;
        if (selectedSlot.quantity <= 0)
            selectedSlot.EmptySlot();
        else
            selectedSlot.UpdateQuantityText(selectedSlot.quantity);
    }

    public void DeselectAllSlots()
    {
        foreach (SellSlots slot in sellSlots)
        {
            slot.selectedShader.SetActive(false);
            slot.isSlotSelected = false;
        }
    }
    
}
