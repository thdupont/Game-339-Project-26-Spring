
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game339.Shared.Services.Implementation;

public class Player : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private SceneTransition _currentZone;
    
    void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void Interact()
    {
        print("Interacted");
        if (_currentZone != null)
            SceneManager.LoadScene(_currentZone.SceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SceneTransition zone))
            _currentZone = zone;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out SceneTransition zone) && zone == _currentZone)
            _currentZone = null;
    }
}