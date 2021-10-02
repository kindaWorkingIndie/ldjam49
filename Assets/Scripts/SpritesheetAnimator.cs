using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float tickRate = 0.2f;
    public bool loop = true;
    public bool playOnAwake = true;

    private float ticker;
    private int currentIndex;
    private bool isPlaying;
    void Start()
    {
        ticker = tickRate;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (playOnAwake) Play();
    }

    void Update()
    {
        if (isPlaying)
        {
            ticker -= Time.deltaTime;
            if (ticker <= 0)
            {
                NextFrame();
                ticker = tickRate;
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    void NextFrame()
    {
        currentIndex++;

        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
            if (!loop)
            {
                isPlaying = false;
                currentIndex = sprites.Length - 1;
            }
        }

        spriteRenderer.sprite = sprites[currentIndex];
    }
}
