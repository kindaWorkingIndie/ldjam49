using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public int shootInterval = 1;
    public int projectileSpeed = 5;

    public enum gunMode {
        SingleDirection,
        CardinalPoints,
        NorthSouth,
        EastWest
    }

    public gunMode equippedGun = gunMode.SingleDirection;

    public Rigidbody2D projectile;

    private float nextShot = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Time.time >= nextShot)
        {
            switch(equippedGun)
            {
                case gunMode.SingleDirection:
                    ShootSingle();
                    break;
                case gunMode.CardinalPoints:
                    ShootCardinalPoints();
                    break;
                case gunMode.NorthSouth:
                    ShootNorthSouth();
                    break;
                case gunMode.EastWest:
                    ShootEastWest();
                    break;
                default: break;
            }
            
            nextShot += shootInterval;
        }
    }

    public void ShootSingle()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Rigidbody2D clone;
        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.left * projectileSpeed);
    }

    public void ShootCardinalPoints()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Rigidbody2D north;
        north = Instantiate(projectile, transform.position, transform.rotation);
        north.velocity = transform.TransformDirection(Vector3.up * projectileSpeed);

        Rigidbody2D east;
        east = Instantiate(projectile, transform.position, transform.rotation);
        east.velocity = transform.TransformDirection(Vector3.right * projectileSpeed);

        Rigidbody2D south;
        south = Instantiate(projectile, transform.position, transform.rotation);
        south.velocity = transform.TransformDirection(Vector3.down * projectileSpeed);

        Rigidbody2D west;
        west = Instantiate(projectile, transform.position, transform.rotation);
        west.velocity = transform.TransformDirection(Vector3.left * projectileSpeed);
    }

    public void ShootNorthSouth()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Rigidbody2D north;
        north = Instantiate(projectile, transform.position, transform.rotation);
        north.velocity = transform.TransformDirection(Vector3.up * projectileSpeed);

        Rigidbody2D south;
        south = Instantiate(projectile, transform.position, transform.rotation);
        south.velocity = transform.TransformDirection(Vector3.down * projectileSpeed);
    }

    public void ShootEastWest()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Rigidbody2D east;
        east = Instantiate(projectile, transform.position, transform.rotation);
        east.velocity = transform.TransformDirection(Vector3.right * projectileSpeed);

        Rigidbody2D west;
        west = Instantiate(projectile, transform.position, transform.rotation);
        west.velocity = transform.TransformDirection(Vector3.left * projectileSpeed);
    }
}
