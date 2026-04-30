using System.Collections.Generic;
using UnityEngine;

public class FishFarm : MonoBehaviour
{
    public int capacity = 2;
    public Transform fishDisplay;
    public GameObject fishIconPrefab;

    private List<FishData> fishInTank = new List<FishData>();

    public bool AddFish(FishData fish)
    {
        if (fishInTank.Count >= capacity)
        {
            return false;
        }

        fishInTank.Add(fish);
        RefreshDisplay();
        return true;
    }

    public bool IsFull() => fishInTank.Count >= capacity;

    private void RefreshDisplay()
    {
        foreach (Transform child in fishDisplay)
            Destroy(child.gameObject);
        
        for (int i = 0; i < fishInTank.Count; i++)
        {
            GameObject icon = Instantiate(fishIconPrefab, fishDisplay);
            
            SpriteRenderer sr = icon.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sprite = fishInTank[i].sprite;
            icon.transform.localPosition = GetSlotPosition(i);
        }
    }

    private Vector3 GetSlotPosition(int index) // claude helped with the math for this one so sorry
    {
        float spacing = 0.5f;
        float startX = -(capacity - 1) * spacing / 2f;
        return new Vector3(startX + index * spacing, 0f, 0f);
    }
}