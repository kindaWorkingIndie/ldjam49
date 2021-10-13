using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : IActivateable
{


    public float activeTime = 1;
    private float activateTimeChangeInterval = 0; // Seconds

    public Sprite ActivatedPlate;
    public Sprite DeactivatedPlate;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //decrement timer
        if (activateTimeChangeInterval > 0)
        {
            activateTimeChangeInterval -= Time.deltaTime;
        }

        //deactivate lever
        if (activateTimeChangeInterval <= 0 && isActivated == true)
        {
            DeactivatePlate();
        }
    }

    void ActivatePlate()
    {
        audioSource.Play();
        spriteRenderer.sprite = ActivatedPlate;
        isActivated = true;

        //activate time
        activateTimeChangeInterval = 5;
    }

    void DeactivatePlate()
    {
        audioSource.Play();
        spriteRenderer.sprite = DeactivatedPlate;
        isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        ActivatePlate();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //activate time
        activateTimeChangeInterval = activeTime;
    }

}
