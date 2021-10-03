using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PingController : MonoBehaviour
{

    public PingLevel[] pingLevels;


    public List<PingLevel> pingQueue;

    public float pingChangeInterval = 10; // Seconds
    private float pingChangeIntervalStore;

    public float realtimePing;
    public float realTimePingInterval = 2; // Seconds

    private static PingController _instance;

    private int currentPingLevelIndex = -1;

    public float lagLevelTimeout = 3;
    private float lagLevelTimeoutStore = 0;
    public bool isOnLevelTimeout = false;

    public static PingController Instance
    {
        get
        {
            return _instance;
        }
    }

    public PingLevel lag { get { return pingQueue[0]; } }
    public PingLevel nextLag { get { return pingQueue.Count >= 2 ? pingQueue[1] : null; } }

    public PingLevel thirdLag { get { return pingQueue.Count >= 3 ? pingQueue[2] : null; } }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        pingChangeIntervalStore = pingChangeInterval;
        pingQueue = new List<PingLevel>();
        AddPingLevelToQueue();
        AddPingLevelToQueue();
        AddPingLevelToQueue();
        realtimePing = lag.delay;

        lagLevelTimeoutStore = lagLevelTimeout;
        lagLevelTimeout = 0;
    }

    void Update()
    {
        if(lagLevelTimeout >= 0)
        {
            // When reaching a new ping level, timeout on the level
            lagLevelTimeout -= Time.deltaTime;
        }
        else
        {
            // When stalled long enough continue
            pingChangeInterval -= Time.deltaTime;
            isOnLevelTimeout = false;
        }


        if (pingChangeInterval < 0)
        {
            pingChangeInterval = pingChangeIntervalStore;
            ChangeLagLevel();

            lagLevelTimeout = lagLevelTimeoutStore;
            isOnLevelTimeout = true;
        }
    }

    void FixedUpdate()
    {
        if(isOnLevelTimeout)
        {
            return;
        }

        float ratio = pingChangeIntervalStore / Time.fixedDeltaTime;
        float pingIncrement = (nextLag.delay - lag.delay) / ratio;

        realtimePing = realtimePing + pingIncrement;
    }

    void AddPingLevelToQueue()
    {
        int newPingLevel = -1;
        while ((newPingLevel=UnityEngine.Random.Range(0, pingLevels.Length))==currentPingLevelIndex);
        currentPingLevelIndex = newPingLevel;
        pingQueue.Add(pingLevels[currentPingLevelIndex]);
    }

    void ChangeLagLevel()
    {
        pingQueue.RemoveAt(0);
        AddPingLevelToQueue();
    }
}
