using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] defaultAnimation;

    public SpriteAnimation[] animations;
    private SpriteAnimation currentAnimation;
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
        SetAnimation();
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

    public void SetAnimation(string name = "")
    {
        if (name == string.Empty)
        {
            currentAnimation = new SpriteAnimation("", defaultAnimation);
            return;
        }
        foreach (var anim in animations)
        {
            if (anim.name == name)
            {
                currentAnimation = anim;
                break;
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

        if (currentIndex >= currentAnimation.sprites.Length)
        {
            currentIndex = 0;
            if (!loop)
            {
                isPlaying = false;
                currentIndex = currentAnimation.sprites.Length - 1;
            }
        }

        spriteRenderer.sprite = currentAnimation.sprites[currentIndex];
    }
}

[System.Serializable]
public class SpriteAnimation
{
    public string name;
    public Sprite[] sprites;

    public SpriteAnimation(string name, Sprite[] sprites)
    {
        this.name = name;
        this.sprites = sprites;
    }
}