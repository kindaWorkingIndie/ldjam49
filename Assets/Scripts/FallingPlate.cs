using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlate : MonoBehaviour
{

    public float breakDelay = .5f;

    private bool isBreaking = false;
    private bool isBroken = false;
    private PlayerKiller playerKiller;
    private SpritesheetAnimator spritesheetAnimator;

    void Start()
    {
        playerKiller = GetComponent<PlayerKiller>();
        spritesheetAnimator = GetComponent<SpritesheetAnimator>();
        playerKiller.enabled = false;
    }

    void Update()
    {
        if (isBreaking && !isBroken)
        {
            breakDelay -= Time.deltaTime;
            if (breakDelay < 0)
            {
                spritesheetAnimator.Play();
                playerKiller.enabled = true;
                isBreaking = false;
                isBroken = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerLagGhost>() && !isBroken)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isBreaking = true;
        }
    }
}
