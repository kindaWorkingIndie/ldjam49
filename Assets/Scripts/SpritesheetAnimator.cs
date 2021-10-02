using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float tickRate;
    private float ticker;
    private int currentIndex;

    void Start()
    {
        ticker = tickRate;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ticker -= Time.deltaTime;
        if (ticker <= 0)
        {
            NextFrame();
            ticker = tickRate;
        }
    }

    void NextFrame()
    {
        currentIndex++;

        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
        }

        spriteRenderer.sprite = sprites[currentIndex];
    }
}
