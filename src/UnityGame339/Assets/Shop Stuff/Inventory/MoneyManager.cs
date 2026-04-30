using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int coins; 
    public Text CoinsText;

    private bool isBuy;
    
    void Start()
    {
        isBuy = false;
        coins = 80;
        CoinsText.text = "$" + coins.ToString();
    }
    
    void Update()
    {
        // update UI
        CoinsText.text = "$" + coins.ToString();
    }

    public bool CanBuy(int itemPrice)
    {
        return coins >= itemPrice;
    }

    public void Buy (int itemPrice)
    {
        // deduct item price from player's coins
        coins = coins - itemPrice;
    }

    public void ShowCoins()
    {
        CoinsText.text = "$" + coins.ToString();
    }
    
}
