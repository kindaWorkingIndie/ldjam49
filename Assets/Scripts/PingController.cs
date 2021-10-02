using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PingController : MonoBehaviour
{

    public PingLevel[] pingLevels;
    public Text pingValue;
    public Text nextPingValue;

    public List<PingLevel> pingQueue;

    public float pingChangeInterval = 10; // Seconds
    private float pingChangeIntervalStore;

    public float realtimePing;
    public float realTimePingInterval = 2; // Seconds
    private float nextTime = 0;

    private static PingController _instance;

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
        pingQueue.Add(pingLevels[UnityEngine.Random.Range(0, pingLevels.Length)]);
        pingQueue.Add(pingLevels[UnityEngine.Random.Range(0, pingLevels.Length)]);
        realtimePing = lag.delay;
        pingValue.text = ((int)Math.Round(realtimePing)).ToString() + " ms";
        nextPingValue.text = nextLag.delay.ToString() + " ms";
    }

    void Update()
    {
        pingChangeInterval -= Time.deltaTime;
        if (pingChangeInterval < 0)
        {
            pingChangeInterval = pingChangeIntervalStore;
            ChangeLagLevel();
        }

        if(Time.time >= nextTime)
        {
            pingValue.text = ((int)Math.Round(realtimePing)).ToString() + " ms";
            nextTime += realTimePingInterval;
        }
    }

    void FixedUpdate()
    {
        float ratio = pingChangeIntervalStore / Time.fixedDeltaTime;
        float pingIncrement = (nextLag.delay - lag.delay) / ratio;

        realtimePing = realtimePing + pingIncrement;
    }

    void ChangeLagLevel()
    {
        pingQueue.RemoveAt(0);
        pingQueue.Add(pingLevels[UnityEngine.Random.Range(0, pingLevels.Length)]);
        nextPingValue.text = nextLag.delay.ToString() + " ms";
    }
}
