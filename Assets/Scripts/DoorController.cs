using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public LeverController Lever;

    public PlateController PressurePlate;

    public Sprite doorOpen;
    public Sprite doorClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Lever.isActivated || PressurePlate.isActivated)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (Lever.isActivated && PressurePlate.isActivated)
            {
                // Open the door
                gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }else{
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
}
