using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public bool killOnTrigger;

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
        if (other.GetComponent<PlayerLagGhost>())
        {
            GameManager.Instance.Die();
        }
    }
}
