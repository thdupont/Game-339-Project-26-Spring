using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange;
    public int amountToChangeStat;
    public int itemID; 
    
    public void UseItem()
    {
        if (statToChange == StatToChange.None)
        {
            // For the Fish Tower and Tier
        }
        
        if (statToChange == StatToChange.FishBreeding)
        {
            // Speeds up fish breeding mechanic
        }
        
        if (statToChange == StatToChange.FishAging)
        {
            // Speeds up fish aging mechanic
        }
        
        if (statToChange == StatToChange.FishFood)
        {
            // If 2 fish have been given food, they will breed
            // Once they are done breeding, they will lose the food they were given
        }
        
    }
    public enum StatToChange
    {
        None,
        FishBreeding,
        FishAging,
        FishFood
    };
    
    
}
