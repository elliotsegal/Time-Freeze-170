using UnityEngine;

public class PlayerOnGroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public Vector2 playerSize;
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

        float distance = (playerSize.y / 2) + checkDistance;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(playerSize.x, 0.01f), 0, Vector2.down, distance - 0.005f, groundLayer);
        onGround = hit.collider != null;
    }

    public void Cooldown()
    {
        cooldown = 0.5f;
    }
}
