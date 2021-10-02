using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SignalLamp : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TurnOn()
    {
        spriteRenderer.sprite = onSprite;
    }

    public void TurnOff()
    {
        spriteRenderer.sprite = offSprite;
    }
}
