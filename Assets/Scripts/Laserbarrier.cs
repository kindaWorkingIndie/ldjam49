using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Laserbarrier : MonoBehaviour
{
    public float onTime = 2;
    public float offTime = 2;
    public bool isOn = true;

    private float ticker;

    private Collider2D col;

    public GameObject[] lasers;


    void Start()
    {
        col = GetComponent<Collider2D>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        ToggleLaser(isOn);
    }

    void Update()
    {
        ticker -= Time.deltaTime;
        if (ticker < 0)
        {
            ticker = isOn ? offTime : onTime;
            isOn = !isOn;
            ToggleLaser(isOn);
        }
    }

    void ToggleLaser(bool on)
    {
        col.enabled = on;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(on);
        }
    }
}
