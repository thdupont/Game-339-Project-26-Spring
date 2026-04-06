using System;
using UnityEngine;
using UnityEngine.UI;
public class TurnBasedFighter : MonoBehaviour
{
    public FishDataObj fishData;
    private Image healthBar;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishData.Health = fishData.MaxHealth;
        healthBar = transform.GetComponentInChildren<Image>();
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitFishFighter()
    {
        setFishSprite();
        UpdateHealthBar();
    }


    private float MapHealthBar(float health)
    {
        if (fishData.MaxHealth <= 0) return 0;
        return Mathf.Clamp01(health / fishData.MaxHealth);
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = MapHealthBar(fishData.Health);
        }
    }
    



    public float GetFishDamage()
    {
        return fishData.AttackDamage;
    }
    
    public void TakeDamage(float damage)
    {
        fishData.Health -= damage;
        UpdateHealthBar();
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
