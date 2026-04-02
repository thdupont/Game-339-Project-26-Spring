using UnityEngine;

[CreateAssetMenu(fileName = "FishDataObj", menuName = "Scriptable Objects/FishDataObj")]
public class FishDataObj : ScriptableObject
{
    public string FishName;
    public string FishDescription;
    public float Health;
    public float AttackDamage = 1f;
    public float MaxHealth;
    public float Defence;

    public float Speed;
    public float FishSize;
    public Sprite FishSprite;
    


}
