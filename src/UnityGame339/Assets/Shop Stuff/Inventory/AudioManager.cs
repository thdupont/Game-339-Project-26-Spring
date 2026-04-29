using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource addItemSfx;
    public AudioSource useItemSfx;
    public AudioSource shopBgMusic;
    
    void Start()
    {
        shopBgMusic.Play();
    }

    public void PlayAddItemSfx()
    {
        addItemSfx.Play();
    }
    
    public void PlayUseItemSfx()
    {
      useItemSfx.Play();
    }
}
