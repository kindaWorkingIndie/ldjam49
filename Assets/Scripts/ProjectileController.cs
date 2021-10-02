using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(!coll.collider.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
