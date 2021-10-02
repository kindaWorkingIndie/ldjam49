
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BridgeController : MonoBehaviour
{

    public float onTime = 2;
    public float offTime = 2;
    private bool isOn = true;
    public float ticker;

    private Collider2D offCollider;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        offCollider = GetComponent<Collider2D>();
        offCollider.isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ticker -= Time.deltaTime;
        if (ticker < 0)
        {
            ticker = isOn ? offTime : onTime;
            isOn = !isOn;
            offCollider.enabled = !isOn;
            spriteRenderer.enabled = isOn;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerLagGhost ghost = other.GetComponent<PlayerLagGhost>();
        if (ghost)
        {
            GameManager.Instance.Respawn();
        }
    }
}
