using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int coins; 
    public Text CoinsText;
    
    void Start()
    {
        coins = 0;
        CoinsText.text = "$" + coins.ToString();
    }
    
    void Update()
    {
        // update UI
        CoinsText.text = "$" + coins.ToString();
    }

    public bool CanBuy(int coins, int itemPrice)
    {
        if (coins >= itemPrice)
            return true;
        else if (coins < itemPrice)
            return false;
        
        return false;
    }

    public int Buy (int coins, int itemPrice)
    {
        // deduct item price from player's coins
        coins = coins - itemPrice;
        return coins;
    }

    public void ShowCoins()
    {
        CoinsText.text = "$" + coins.ToString();
    }
    
}
