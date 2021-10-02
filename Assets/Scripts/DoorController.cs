using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public List<IActivateable> activators;
    public List<SignalLamp> signalLamps;

    public Sprite doorOpen;
    public Sprite doorClose;

    void Start()
    {
        if (activators.Count != signalLamps.Count)
        {
            Debug.LogError("Length of SignalLamps is not equal to the Activators Length");
        }
    }

    void Update()
    {
        UpdateSignalLamps();
        if (isAnythingActivated())
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (isEverythingActivated())
            {
                // Open the door
                gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                // Close the door
                gameObject.GetComponent<SpriteRenderer>().sprite = doorClose;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorClose;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public bool isOpen()
    {
        return isEverythingActivated();
    }

    bool isEverythingActivated()
    {
        foreach (IActivateable activator in activators)
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
        foreach (IActivateable activator in activators)
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
            IActivateable activator = activators[index];
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
