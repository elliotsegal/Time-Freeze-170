using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    private Rigidbody2D body;
    private Vector2 input;
    private bool jump;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jump |= Input.GetButtonDown("Jump");
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
            velocity.y = jumpSpeed;
            jump = false;
        }
        body.velocity = velocity;
    }
}
