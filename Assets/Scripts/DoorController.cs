using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public LeverController Lever;

    public PlateController PressurePlate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Lever.isActivated || PressurePlate.isActivated)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (Lever.isActivated && PressurePlate.isActivated)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
