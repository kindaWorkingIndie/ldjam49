using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 6;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Rigidbody2D rb;

    public PlayerLagGhost ghost;

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
        if (x != 0)
        {
            y = 0;
        }
        else if (y != 0)
        {
            x = 0;
        }
        moveInput = new Vector2(x, y);
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * walkSpeed;
    }
}
