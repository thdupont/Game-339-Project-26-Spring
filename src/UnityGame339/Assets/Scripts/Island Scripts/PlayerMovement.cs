using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator animator;

    void FixedUpdate()
    {
        // Replace Input.GetAxis with the new Input System equivalents
        float horizontal = Keyboard.current != null ? 
            (Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue()) : 0f;
        float vertical = Keyboard.current != null ? 
            (Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) : 0f;

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        animator.SetFloat("horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * 5f;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}