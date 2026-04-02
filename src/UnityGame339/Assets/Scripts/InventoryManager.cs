using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;

    private bool isActivated;
    
    //Needs to Open the menu on button press
    //Needs to Pause the game while the menu is open
    void Update()
    {
        // Open Inventory with "Q"
        if (Input.GetKeyDown(KeyCode.Q) && !isActivated)
        {
            InventoryMenu.SetActive(true);
            isActivated = true;
        }
        
        else if (Input.GetKeyDown(KeyCode.Q) && isActivated)
        {
            InventoryMenu.SetActive(false);
            isActivated = false;
        }
    }

}
