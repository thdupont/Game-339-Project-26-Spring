using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Player Player;
    
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.eKey.isPressed)
        {
            Player.Interact();
        }
    }
}