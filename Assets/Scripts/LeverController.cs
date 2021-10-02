using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActivated = !isActivated;
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.green){
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }else{
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }
}
