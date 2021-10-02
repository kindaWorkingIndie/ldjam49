using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingController : MonoBehaviour
{

    public int[] pingLevels;
    public int currentPingLevel = 0;

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

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        pingChangeIntervalStore = pingChangeInterval;
    }

    void Update()
    {
        pingChangeInterval -= Time.deltaTime;
        if (pingChangeInterval < 0)
        {
            pingChangeInterval = pingChangeIntervalStore;
            // Change lag level
            ChangeLagLevel();
        }

    }

    void ChangeLagLevel()
    {

    }
}
