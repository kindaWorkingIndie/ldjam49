using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public bool killOnTrigger;
    public DeathCause deathCause = DeathCause.Unknown;

    private Collider2D coll;

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerLagGhost>() && this.enabled)
        {
            GameManager.Instance.Die(deathCause);
        }
    }
}
