using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLagGhost : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed = 6;
    public int neededBugs = 5;

    PingController pingController;
    private Queue<GhostCommand> commandQueue;

    private float lagDelay = 0;
    private int interval = 30;
    private Animator animator;

    private bool executeCommands = true;

    private int bugCounter = 0;


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
        interval = (interval - 1) % 30;

        if (lagDelay < 0)
        {
            lagDelay = pingController.realtimePing / 1000; // Dirty seconds to ms convertion
            if (commandQueue.Count != 0 && executeCommands)
            {
                Vector2 pos = commandQueue.Dequeue().move;
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
            animator.SetBool("moving", commandQueue.Count != 0);
        }

        if(commandQueue.Count != 0 && interval == 0 && executeCommands)
        {
            commandQueue.Dequeue();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Projectile"))
        {
            Destroy(coll.gameObject);
            GameManager.Instance.Die();
        }

        if(coll.gameObject.CompareTag("BugWall") && pingController.lag.isBuggy)
        {
            BugThrough(coll);
        }
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

            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal") * (2 * wall.bounds.size.x),
                                        Input.GetAxisRaw("Vertical") * (2 * wall.bounds.size.y),
                                        0);
            Vector3 newPos = transform.position + dir;
            rb.MovePosition(newPos);

            if(bugCounter >= neededBugs)
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
