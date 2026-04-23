using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class FishContainer
{
    private static List<FishDataObj> allFishData;
    //temp here
    private static List<FishDataObj> playerFish = new List<FishDataObj>();

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

    public static FishDataObj GetRandomFish()
    {
        return allFishData[Random.Range(0, allFishData.Count)];
    }

    public static void Refresh()
    {
        LoadAllFish();
    }

    public static FishDataObj GetPlayerFishByName(string name)
    {
        return allFishData.FirstOrDefault(f => f.FishName == name);
    }

    public static List<FishDataObj> GetAllPlayerFish()
    {
        return new List<FishDataObj>(allFishData);
    }

    public static void AddFishToPlayer(FishDataObj fishDataObj)
    {
        if (playerFish == null)
        {
            playerFish = new List<FishDataObj>();
        }
        //check if less then 6 fish
        if (playerFish.Count < 6)
        {
            playerFish.Add(fishDataObj);
            Debug.Log($"Added {fishDataObj.FishName} to player inventory. Total: {playerFish.Count}");
        }
        else
        {
            Debug.Log("Player inventory full!");
        }
    }
    
    
}
