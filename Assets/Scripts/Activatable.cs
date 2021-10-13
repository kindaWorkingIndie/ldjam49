using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Activatable : MonoBehaviour
{
    public enum ActivatableType
    {
        LEVER,
        PRESSURE_PLATE,
    }

    public ActivatableType activatableType;

    [Tooltip("If 'timeActivated' is '0' it will be infinite")]
    public float timeActivated = 0;


    private float timer;
    private bool inReach;
    private bool _isActivated;
    public bool isActivated { get { return _isActivated; } }
    private Collider2D myCollider;
    [Header("Sprites (Optional)")]
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;
    private SpriteRenderer spriteRenderer;
    [Header("Sprites (Optional)")]
    public AudioClip activateSound;
    public AudioClip deactivateSound;
    private AudioSource audioSource;

    [Header("Events (Optional)")]
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (activatedSprite == null)
            {
                Debug.LogError("A SpriteRenderer is set on " + gameObject.name + ", but there is no 'activatedSprite' set");
            }
            if (deactivatedSprite == null)
            {
                Debug.LogError("A SpriteRenderer is set on " + gameObject.name + ", but there is no 'deactivatedSprite' set");
            }
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            if (activateSound == null)
            {
                Debug.LogError("A AudioSource is set on " + gameObject.name + ", but there is no 'activateSound' set");
            }
            if (deactivateSound == null)
            {
                Debug.LogError("A AudioSource is set on " + gameObject.name + ", but there is no 'deactivateSound' set");
            }
        }

    }

    void Update()
    {
        if (timeActivated != 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Deactivate();
            }
        }

        switch (activatableType)
        {
            case ActivatableType.LEVER:
                if (inReach && Input.GetButtonDown("Interact"))
                {
                    Activate();
                }
                break;
            case ActivatableType.PRESSURE_PLATE:
                if (inReach)
                {
                    timer = timeActivated;
                }
                break;
        }
    }


    void Activate()
    {

        onActivate?.Invoke();

        if (spriteRenderer)
        {
            spriteRenderer.sprite = activatedSprite;
        }
        if (audioSource)
        {
            audioSource.clip = activateSound;
        }
        timer = timeActivated;
        _isActivated = true;
    }

    void Deactivate()
    {

        onDeactivate?.Invoke();

        if (spriteRenderer)
        {
            spriteRenderer.sprite = deactivatedSprite;
        }
        if (audioSource)
        {
            audioSource.clip = deactivateSound;
        }
        _isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isPlayer(coll))
        {
            inReach = true;
            if (activatableType == ActivatableType.PRESSURE_PLATE)
            {
                Activate();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (isPlayer(coll))
        {
            inReach = false;
        }
    }

    bool isPlayer(Collider2D col)
    {
        return col.GetComponent<PlayerController>() || col.GetComponent<PlayerLagGhost>();
    }
}
