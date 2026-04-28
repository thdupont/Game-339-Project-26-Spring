using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource addItemSFX;
    public AudioSource useItemSFX;
    public AudioSource shopBGMusic;
    
    void Start()
    {
        shopBGMusic = GetComponent<AudioSource>();
        shopBGMusic.Play();
    }

    public void PlayAddItemSFX()
    {
        addItemSFX = GetComponent<AudioSource>();
        addItemSFX.Play();
    }
    
    public void PlayUseItemSFX()
    {
      useItemSFX = GetComponent<AudioSource>();
      useItemSFX.Play();
    }
}
