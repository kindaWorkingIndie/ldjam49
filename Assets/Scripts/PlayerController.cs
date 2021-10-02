using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 6;
    public int maxIdleTicks = 5;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Rigidbody2D rb;

    public PlayerLagGhost ghost;
    private Animator animator;

    private enum moveDirection
    {
        none,
        vertical,
        horizontal
    }

    private int idleCounter = 0;

    private moveDirection lastMoveDir = moveDirection.none;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (!ghost)
        {
            Debug.LogError("No ghost attached");
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        switch (lastMoveDir)
        {
            case moveDirection.horizontal:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    lastMoveDir = moveDirection.vertical;
                }
                break;
            case moveDirection.vertical:
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    lastMoveDir = moveDirection.horizontal;
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    lastMoveDir = moveDirection.vertical;
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    lastMoveDir = moveDirection.horizontal;
                }
                break;
        }

        float _x = lastMoveDir == moveDirection.horizontal ? x : 0;
        float _y = lastMoveDir == moveDirection.vertical ? y : 0;

        if (_x == 0 && _y == 0)
        {
            lastMoveDir = moveDirection.none;
        }
        moveInput = new Vector2(x, y);
        rb.velocity = moveInput * walkSpeed;
        animator.SetBool("moving", moveInput != Vector2.zero);
        animator.SetFloat("horizontal", x < 0 ? -1 : 1);
    }

    void FixedUpdate()
    {
        if (lastMoveDir != moveDirection.none)
        {
            GhostCommand cmd = new GhostCommand();
            cmd.move = rb.position;
            SendGhostInformation(cmd);

            idleCounter = 0;
        }
        else
        {
            if (idleCounter <= maxIdleTicks)
            {
                ++idleCounter;
                GhostCommand cmd = new GhostCommand();
                cmd.move = rb.position;
                SendGhostInformation(cmd);
            }
        }
    }

    void SendGhostInformation(GhostCommand cmd)
    {
        ghost.PushCommand(cmd);
    }
}
