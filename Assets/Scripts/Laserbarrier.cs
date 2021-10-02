using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Laserbarrier : MonoBehaviour
{
    public float onTime = 2;
    public float offTime = 2;
    private bool isOn = true;
    public float ticker;

    private Collider2D col;

    public GameObject[] lasers;


    void Start()
    {
        col = GetComponent<Collider2D>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Update()
    {
        ticker -= Time.deltaTime;
        if (ticker < 0)
        {
            ticker = isOn ? offTime : onTime;
            isOn = !isOn;
            col.enabled = isOn;
            foreach (GameObject laser in lasers)
            {
                laser.SetActive(isOn);
            }


        }
    }
}
