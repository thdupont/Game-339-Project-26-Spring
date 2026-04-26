using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject [] persistentObjects = new GameObject[2]; // shared list between all objects

    public int objectIndex;
    
    
    // only works on parent objects!
    void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
        
    }

    
}
