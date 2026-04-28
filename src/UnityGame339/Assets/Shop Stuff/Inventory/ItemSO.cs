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
        if (statToChange == StatToChange.fishing)
        {
            // for bait, this would allow the player to participate in the fishing minigame
            // the player needs bait to fish basically
            Debug.Log("You changed: fishing");
        }

        if (statToChange == StatToChange.attack)
        {
            //Debug.Log("You used the fishing rod+. Nothing happened.");
        }

        if (statToChange == StatToChange.speed)
        {
            // this is a place holder for the skill token, I'll make multiple for each stat
            //Debug.Log("You used the skill token. Nothing happened.");
        }
    }
    public enum StatToChange
    {
        none,
        fishing,
        health, // add check to see if health is maxed before using item
        attack,
        defense,
        speed
    };
    
    
}
