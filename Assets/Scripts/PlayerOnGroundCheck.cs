using UnityEngine;

public class PlayerOnGroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public float playerHeight;
    public float checkDistance;

    public bool onGround { get; private set; }

    private float cooldown;

    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            onGround = false;
            cooldown -= Time.fixedDeltaTime;
            return;
        }

        float distance = (playerHeight / 2) + checkDistance;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        onGround = hit.collider != null;
    }

    public void Cooldown()
    {
        cooldown = 0.5f;
    }
}
