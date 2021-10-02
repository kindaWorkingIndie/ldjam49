using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLagGhost : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed = 6;

    PingController pingController;
    private Queue<GhostCommand> commandQueue;

    private float lagDelay = 0;
    private int interval = 30;
    private Animator animator;


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
            lagDelay = pingController.lag.delay / 1000; // Dirty seconds to ms convertion
            if (commandQueue.Count != 0)
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

        if (commandQueue.Count != 0 && interval == 0)
        {
            commandQueue.Dequeue();
        }
    }

    public void PushCommand(GhostCommand cmd)
    {
        commandQueue.Enqueue(cmd);
    }

    public void Move()
    {

    }
}
