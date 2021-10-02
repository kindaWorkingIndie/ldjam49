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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pingController = FindObjectOfType<PingController>();
        commandQueue = new Queue<GhostCommand>();
    }

    // Update is called once per frame
    void Update()
    {
        lagDelay -= Time.deltaTime;
        interval = (interval - 1) % 30;
        Debug.Log(lagDelay);
        if(lagDelay < 0)
        {
            lagDelay = pingController.lag.delay / 1000; // Dirty seconds to ms convertion
            if(commandQueue.Count != 0)
            {
                Vector2 pos = commandQueue.Dequeue().move;
                rb.MovePosition(pos);
            }
        }

        if(commandQueue.Count != 0 && interval == 0)
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
