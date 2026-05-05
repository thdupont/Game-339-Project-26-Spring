using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

  
    public void ExitGame()
    {
        Application.Quit();
    }
}
