using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{


    public void ResetAfterAnimation()
    {
        Transform spriteChild = transform.GetComponentInChildren<SpriteRenderer>().transform;
        spriteChild.localScale = Vector3.one;
        spriteChild.rotation = Quaternion.Euler(Vector3.zero);
    }
    public void ResetAndHideAfterAnimation()
    {
        ResetAfterAnimation();
    }
}
