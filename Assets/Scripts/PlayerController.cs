using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    [NonSerialized] public Color color;

    private Rigidbody2D body;
    private PlayerOnGroundCheck groundCheck;

    private Vector2 input;
    private bool jump;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<PlayerOnGroundCheck>();
        GetComponent<Renderer>().material.color = color;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = body.velocity;
        bool running = Mathf.Abs(input.x) > 0.5f;
        if (running)
        {
            velocity.x = Mathf.Sign(input.x) * runSpeed;
        }
        else
        {
            velocity.x = 0;
        }
        if (jump)
        {
            if (groundCheck.onGround)
            {
                velocity.y = jumpSpeed;
                groundCheck.Cooldown();
            }
            jump = false;
        }
        body.velocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jump = true;
    }
}
