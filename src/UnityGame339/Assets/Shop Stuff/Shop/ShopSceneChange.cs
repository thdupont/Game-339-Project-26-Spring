using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneChange : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
