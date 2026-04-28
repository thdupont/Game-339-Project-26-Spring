using UnityEngine;
using UnityEngine.InputSystem;

public class FishFarmTester : MonoBehaviour
{
    public FishFarm fishFarm;
    public FishData testFish;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            bool added = fishFarm.AddFish(testFish);

            if (added)
                Debug.Log("Fish added");
            else
                Debug.Log("Tank full");
        }
    }
}