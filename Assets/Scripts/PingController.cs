using System.Collections.Generic;
using UnityEngine;
public class PingController : MonoBehaviour
{

    public PingLevel[] pingLevels;

    public List<PingLevel> pingQueue;

    public float pingChangeInterval = 5; // Seconds
    public float pingChangeIntervalStore;

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
        pingQueue.Add(pingLevels[Random.Range(0, pingLevels.Length)]);
        pingQueue.Add(pingLevels[Random.Range(0, pingLevels.Length)]);
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

    void ChangeLagLevel()
    {
        pingQueue.RemoveAt(0);
        pingQueue.Add(pingLevels[Random.Range(0, pingLevels.Length)]);
    }
}
