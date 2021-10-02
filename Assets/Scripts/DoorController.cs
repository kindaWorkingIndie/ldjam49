using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public List<IActivateable> activators;


    public Sprite doorOpen;
    public Sprite doorClose;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
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
}
