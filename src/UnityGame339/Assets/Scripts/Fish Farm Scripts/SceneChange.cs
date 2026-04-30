using UnityEngine;
using UnityEngine.SceneManagement;


public class FishFarmSceneChange : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainIsland");
    }
}
