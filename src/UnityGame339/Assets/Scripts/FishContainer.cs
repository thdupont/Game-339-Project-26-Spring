using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class FishContainer
{
    private static List<FishDataObj> allFishData;

    static FishContainer()
    {
        LoadAllFish();
    }

    private static void LoadAllFish()
    {
        // For now, we load them from Resources.
        // If they aren't in a Resources folder, we can either move them or use AssetDatabase in editor.
        // But for runtime access, they should be in Resources or via a Serialized list in a MonoBehaviour.
        // The prompt says "static class", suggesting easy access.
        allFishData = Resources.LoadAll<FishDataObj>("FishData").ToList();
        Debug.Log(allFishData.Count);
        Debug.Log("Fish Loaded" + allFishData);
        
        if (allFishData.Count == 0)
        {
            // Fallback: try loading from root Resources if "FishData" subfolder doesn't exist or is empty
            allFishData = Resources.LoadAll<FishDataObj>("").ToList();
            
        }
        
    }

    public static FishDataObj GetFishByName(string name)
    {
        return allFishData.FirstOrDefault(f => f.FishName == name);
    }

    public static List<FishDataObj> GetAllFish()
    {
        return new List<FishDataObj>(allFishData);
    }

    public static void Refresh()
    {
        LoadAllFish();
    }
}
