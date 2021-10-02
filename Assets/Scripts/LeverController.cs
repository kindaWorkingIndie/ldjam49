using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false;

    private bool inReach = false;

    public float activateTimeChangeInterval = 0; // Seconds

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //decrement timer
        if (activateTimeChangeInterval>0){
            activateTimeChangeInterval -= Time.deltaTime;
            Debug.Log(Time.deltaTime);
        }
        
        //deactivate lever
        if (activateTimeChangeInterval <= 0)
        {
            DeactivateLever();
        }

        if (Input.GetKeyDown(KeyCode.E)&& inReach)
        {
            // activate or deactivate lever
            ActivateLever();
        }
    }

    void ActivateLever()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        isActivated = true;

        //activate time
        activateTimeChangeInterval = 5;
    }

    void DeactivateLever()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            inReach = true;
        } 
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            inReach = false;
        }
        
    }

}
