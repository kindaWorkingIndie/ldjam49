using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : IActivateable
{

    private bool inReach = false;

    public float activateTimeChangeInterval = 0; // Seconds

    public Sprite ActivatedLever;
    public Sprite DeactivatedLever;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Start()
    {

        Debug.LogError("LeverController is deprecated. Please use Activatable instead on " + gameObject.name);
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
        if (activateTimeChangeInterval <= 0 && isActivated)
        {
            DeactivateLever();
        }

        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {
            ActivateLever();
        }
    }

    void ActivateLever()
    {
        audioSource.Play();
        spriteRenderer.sprite = ActivatedLever;
        isActivated = true;

        //activate time
        activateTimeChangeInterval = 5;
    }

    void DeactivateLever()
    {
        audioSource.Play();
        spriteRenderer.sprite = DeactivatedLever;
        isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>() || coll.GetComponent<PlayerLagGhost>())
        {
            inReach = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>() || coll.GetComponent<PlayerLagGhost>())
        {
            inReach = false;
        }
    }

}
