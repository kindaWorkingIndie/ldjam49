using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    private Collider2D col;


    void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }


    void SetCheckpoint()
    {
        GameManager.Instance.SetCheckpoint(this);
        GetComponent<SpritesheetAnimator>().SetAnimation("activated");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerLagGhost>())
        {
            SetCheckpoint();
        }
    }

}
