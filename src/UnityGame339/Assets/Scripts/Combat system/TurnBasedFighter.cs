using UnityEngine;

public class TurnBasedFighter : MonoBehaviour
{
    public FishDataObj fishData;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishData.Health = fishData.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitFishFighter()
    {
        setFishSprite();
    }



    public float GetFishDamage()
    {
        return fishData.AttackDamage;
    }
    
    public void TakeDamage(float damage)
    {
        fishData.Health -= damage;
    }
    
    

    private void setFishData(FishDataObj fishData)
    {
        this.fishData = fishData;
    }

    private void setFishSprite()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fishData.FishSprite;
    }

    
}
