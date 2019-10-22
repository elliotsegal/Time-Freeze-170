using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public const int Layer = 8;

    public float runSpeed;
    public float jumpSpeed;
    public float timeMultiplier;
    [Space]
    public ParticleSystem timeStopFX;

    [NonSerialized] public Color color;

    private Rigidbody2D body;
    private Animator animator;
    private PlayerOnGroundCheck groundCheck;
    private PlayerTimer timer;
    private bool frozen;
    private Vector2 savedVelocity;

    private bool freezingOtherPlayers;
    [NonSerialized] public int hazardsOverlapping;

    private Vector2 input;
    private bool jump;

    public void Init()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<PlayerOnGroundCheck>();
        timer = GetComponentInChildren<PlayerTimer>();
    }

    private void Start()
    {
        GetComponentInChildren<Renderer>().material.color = color;
        UpdateFacing(false);
    }

    private void UpdateFacing(bool left)
    {
        transform.GetChild(0).localEulerAngles = new Vector3(0, left ? -90 : 90, 0);
    }

    private void Update()
    {
        bool running = Mathf.Abs(input.x) > 0.5f;
        animator.SetBool("On Ground", groundCheck.onGround);
        animator.SetBool("Walk", running);
        animator.speed = frozen ? 0 : 1;

        if (frozen)
            timeMultiplier = 0;
        else
        {
            timeMultiplier = 1;
            if (hazardsOverlapping > 0)
                timeMultiplier *= 1.5f;
            if (freezingOtherPlayers)
                timeMultiplier *= 2;
        }
    }

    private void FixedUpdate()
    {
        if (frozen) return;
        Vector2 velocity = body.velocity;
        bool running = Mathf.Abs(input.x) > 0.5f;
        if (running)
        {
            velocity.x = Mathf.Sign(input.x) * runSpeed;
            UpdateFacing(input.x < 0);
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


    public void SetFrozen(bool frozen, PlayerController source)
    {
        body.isKinematic = frozen;
        timeStopFX.gameObject.SetActive(frozen);
        if (frozen)
        {
            ParticleSystem.MainModule main = timeStopFX.main;
            main.startColor = source.color;
        }

        if (frozen && !this.frozen)
        {
            this.frozen = true;
            savedVelocity = body.velocity;
            body.velocity = Vector2.zero;
        }
        else if (!frozen && this.frozen)
        {
            this.frozen = false;
            body.velocity = savedVelocity;
        }
    }

    public void OnPickUp(Powerup powerup)
    {
        timer.AddTime(10);
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

    public void OnFreeze(InputAction.CallbackContext context)
    {
        if (frozen || context.started) return;
        if (context.performed)
        {
            foreach (PlayerController player in PlayerManager.singleton.GetOtherPlayers(this))
            {
                player.SetFrozen(true, this);
            }
            freezingOtherPlayers = true;
        }
        else
        {
            foreach (PlayerController player in PlayerManager.singleton.GetOtherPlayers(this))
            {
                player.SetFrozen(false, this);
            }
            freezingOtherPlayers = false;
        }
    }
}
