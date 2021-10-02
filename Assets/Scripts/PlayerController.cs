using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 6;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Rigidbody2D rb;

    public PlayerLagGhost ghost;

    private enum moveDirection {
        none,
        vertical,
        horizontal
    }

    private moveDirection lastMoveDir = moveDirection.none;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!ghost)
        {
            Debug.LogError("No ghost attached");
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        switch(lastMoveDir)
        {
            case moveDirection.horizontal:
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    lastMoveDir = moveDirection.vertical;
                }
                break;
            case moveDirection.vertical:
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    lastMoveDir = moveDirection.horizontal;
                }
                break;
            default:
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    lastMoveDir = moveDirection.vertical;
                }
                else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    lastMoveDir = moveDirection.horizontal;
                }
                break;
        }

        x = lastMoveDir == moveDirection.horizontal ? x : 0;
        y = lastMoveDir == moveDirection.vertical ? y : 0;

        moveInput = new Vector2(x, y);
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * walkSpeed;
    }
}
