using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLagGhost : PlayerCharacter
{
    private Rigidbody2D rb;
    public int neededBugs = 5;

    PingController pingController;
    private Queue<GhostCommand> commandQueue;

    private float lagDelay = 0;
    private int interval = 40;
    [HideInInspector]
    public Animator animator;

    private bool executeCommands = true;

    private int bugCounter = 0;

    private Vector3 lastLegalPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pingController = FindObjectOfType<PingController>();
        commandQueue = new Queue<GhostCommand>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        lagDelay -= Time.deltaTime;
        interval = (interval - 1) % 40;

        if (lagDelay < 0)
        {
            lagDelay = (pingController.realtimePing * 0.5f) / 1000; // Dirty seconds to ms convertion
            if (commandQueue.Count != 0 && executeCommands)
            {
                Vector2 pos = commandQueue.Dequeue().move;

                if (CheckIllegalMove(pos))
                {
                    rb.MovePosition(pos);
                    if (pos.x > 0)
                    {
                        animator.SetFloat("horizontal", 1);
                    }
                    else
                    {
                        animator.SetFloat("horizontal", -1);
                    }
                }
            }
            animator.SetBool("moving", commandQueue.Count != 0);
        }

        // Manually forget some Steps from the queue to make it look laggy
        if (commandQueue.Count != 0 && interval == 0 && executeCommands)
        {
            commandQueue.Dequeue();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Projectile"))
        {
            Destroy(coll.gameObject);
            GameManager.Instance.Die(DeathCause.Electrocuted);
        }

        if (coll.gameObject.CompareTag("BugWall") && pingController.lag.isBuggy)
        {
            BugThrough(coll);
        }
    }

    private bool CheckIllegalMove(Vector3 pos)
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(pos, 0.08f);
        foreach (Collider2D c in coll)
        {
            if (c.gameObject.GetComponent<DoorController>() != null)
            {
                GameManager.Instance.SnapPlayerToGhost(lastLegalPosition);
                commandQueue.Clear();
                return false;
            }
        }

        lastLegalPosition = pos;
        return true;
    }

    public void PushCommand(GhostCommand cmd)
    {
        if (commandQueue == null) return;
        commandQueue.Enqueue(cmd);
    }

    private void BugThrough(Collider2D wall)
    {
        executeCommands = false;
        {
            Rigidbody2D playerRb = GameManager.Instance.player.GetComponent<Rigidbody2D>();

            Debug.Log(Input.GetAxisRaw("Vertical"));
            ++bugCounter;

            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal") * (1.5f * wall.bounds.size.x),
                                        Input.GetAxisRaw("Vertical") * (1.5f * wall.bounds.size.y),
                                        0);
            Vector3 newPos = transform.position + dir;
            rb.MovePosition(newPos);

            if (bugCounter >= neededBugs)
            {
                GameManager.Instance.SnapPlayerToGhost(newPos);
                commandQueue.Clear();
                bugCounter = 0;
            }
        }
        executeCommands = true;
    }

    public void ClearQueue()
    {
        commandQueue.Clear();
    }
}
