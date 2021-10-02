using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HoloHint : MonoBehaviour
{
    public string text;

    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerLagGhost>() || other.GetComponent<PlayerController>())
        {
            UIManager.Instance.ShowHint(text);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerLagGhost>() || other.GetComponent<PlayerController>())
        {
            UIManager.Instance.HideHint();
        }
    }
}
