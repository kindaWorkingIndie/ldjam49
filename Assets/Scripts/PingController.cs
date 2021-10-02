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
    private float nextTime = 0;

    private static PingController _instance;

    private int currentPingLevelIndex = -1;

    public static PingController Instance
    {
        get
        {
            return _instance;
        }
    }

    public PingLevel lag { get { return pingQueue[0]; } }
    public PingLevel nextLag { get { return pingQueue.Count >= 2 ? pingQueue[1] : null; } }

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
        realtimePing = lag.delay;
    }

    void Update()
    {
        pingChangeInterval -= Time.deltaTime;
        if (pingChangeInterval < 0)
        {
            pingChangeInterval = pingChangeIntervalStore;
            ChangeLagLevel();
        }
    }

    void FixedUpdate()
    {
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
