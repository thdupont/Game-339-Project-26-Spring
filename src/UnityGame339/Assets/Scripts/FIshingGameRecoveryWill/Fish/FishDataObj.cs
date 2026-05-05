using UnityEngine;

[CreateAssetMenu(fileName = "FishDataObj", menuName = "Scriptable Objects/FishDataObj")]
public class FishDataObj : ScriptableObject
{
    public string FishName;
    public string FishDescription;
    public float fishValue = 1f;
    public float Speed;
    public float FishSize;
    public Sprite FishSprite;
    


}
