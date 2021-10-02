using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLagGhost : MonoBehaviour
{

    PingController pingController;

    // Start is called before the first frame update
    void Start()
    {
        pingController = FindObjectOfType<PingController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {

    }
}
