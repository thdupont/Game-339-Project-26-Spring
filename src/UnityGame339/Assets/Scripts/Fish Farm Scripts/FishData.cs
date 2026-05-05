using UnityEngine;

[CreateAssetMenu(fileName = "NewFishObject", menuName = "Fish Farm/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public Sprite sprite;
    public int value;
}