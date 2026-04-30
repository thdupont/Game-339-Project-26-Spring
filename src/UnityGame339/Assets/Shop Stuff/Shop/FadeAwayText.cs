using UnityEngine;
using UnityEngine.UI;

public class FadeAwayText : MonoBehaviour
{
    private float fadeTime;
    private Text fadeAwayText;
    
    void Start()
    {
        fadeAwayText = GetComponent<Text>();
        fadeTime = 0;
    }
    
    void Update()
    {
        if (fadeTime > 0)
        {
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
            fadeTime -= Time.deltaTime;
        }

        if (fadeTime <= 0)
        {
            fadeAwayText.enabled = false;
        }
    }

    public void MakeTextVisible()
    {
        fadeAwayText.enabled = true;
        fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, 1);
        fadeTime = 2;
    }
}
