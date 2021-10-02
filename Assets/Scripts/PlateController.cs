using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : IActivateable
{


    public float activateTimeChangeInterval = 0; // Seconds

    public Sprite ActivatedPlate;

    public Sprite DeactivatedPlate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //decrement timer
        if (activateTimeChangeInterval > 0)
        {
            activateTimeChangeInterval -= Time.deltaTime;
        }

        //deactivate lever
        if (activateTimeChangeInterval <= 0)
        {
            DeactivatePlate();
        }
    }

    void ActivatePlate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ActivatedPlate;
        isActivated = true;

        //activate time
        activateTimeChangeInterval = 5;
    }

    void DeactivatePlate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DeactivatedPlate;
        isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        ActivatePlate();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //activate time
        activateTimeChangeInterval = 1;
    }

}
