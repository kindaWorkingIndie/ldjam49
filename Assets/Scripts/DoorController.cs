using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public List<Activatable> activators;
    public List<SignalLamp> signalLamps;

    public Sprite doorOpen;
    public Sprite doorClose;
    private bool open = false;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;

    void Start()
    {
        if (activators.Count != signalLamps.Count)
        {
            Debug.LogError("Length of SignalLamps is not equal to the Activators Length");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateSignalLamps();
        if (isAnythingActivated())
        {
            if (isEverythingActivated())
            {
                if (!open)
                {
                    audioSource.Play();
                    // Open the door
                    spriteRenderer.sprite = doorOpen;
                    boxCollider.enabled = false;
                    open = true;
                }
            }
            else
            {
                if (open)
                {
                    // Close the door
                    spriteRenderer.sprite = doorClose;
                    boxCollider.enabled = true;
                    open = false;
                }
            }
        }
        else
        {
            spriteRenderer.sprite = doorClose;
            boxCollider.enabled = true;
            open = false;
        }
    }

    public bool isOpen()
    {
        return open;
    }

    bool isEverythingActivated()
    {
        foreach (Activatable activator in activators)
        {
            if (!activator.isActivated)
            {
                return false;
            }
        }
        return true;
    }
    bool isAnythingActivated()
    {
        foreach (Activatable activator in activators)
        {
            if (activator.isActivated)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateSignalLamps()
    {
        for (int index = 0; index < activators.Count; index++)
        {
            Activatable activator = activators[index];
            SignalLamp lamp = signalLamps[index];
            if (activator.isActivated)
            {
                lamp.TurnOn();
            }
            else
            {
                lamp.TurnOff();
            }
        }
    }
}
