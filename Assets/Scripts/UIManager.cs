using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
    [Header("Ping")]
    public Image pingDisplayImage;
    public Image nextPingImage;
    public Image thirdPingImage;
    public Text pingValue;
    public Text nextPingValue;
    public Text thirdPingValue;
    public Text runTimer;
    private float pingNotifyTimeout = 0;

    [Header("Hints")]
    public GameObject hintPanel;
    public Text hintText;
    public Text screenHintText;

    public float screenHintTime = 2f;
    private float screenHintTimeStore;



    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        screenHintTimeStore = screenHintTime;
    }

    void Start()
    {
        screenHintText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (PingController.Instance.pingQueue.Count == 0) return;
        pingDisplayImage.color = PingController.Instance.lag.color;
        if (PingController.Instance.nextLag != null)
        {
            nextPingImage.color = PingController.Instance.nextLag.color;
            nextPingValue.text = PingController.Instance.nextLag.delay.ToString() + " ms";
        }
        if (PingController.Instance.thirdLag != null)
        {
            thirdPingImage.color = PingController.Instance.thirdLag.color;
            thirdPingValue.text = PingController.Instance.thirdLag.delay.ToString() + " ms";
        }

        if (Time.time >= pingNotifyTimeout)
        {
            pingNotifyTimeout += PingController.Instance.realTimePingInterval;
            pingValue.text = ((int)Mathf.Round(PingController.Instance.realtimePing)).ToString() + " ms";
        }

        runTimer.text = TimeSpan.FromSeconds(GameManager.Instance.GetCurrentRunTime()).ToString("mm':'ss':'fff");

        ScreenHintUpdate();
    }

    void ScreenHintUpdate()
    {
        if (screenHintText.gameObject.activeSelf)
        {
            screenHintTime -= Time.deltaTime;
            if (screenHintTime < 0)
            {
                screenHintTime = screenHintTimeStore;
                screenHintText.gameObject.SetActive(false);
            }
        }
    }

    public void ShowHint(string hint)
    {
        hintPanel.SetActive(true);
        hintText.text = hint;
    }

    public void HideHint()
    {
        hintPanel.SetActive(false);
        hintText.text = "";
    }

    public void ShowScreenHint(string text)
    {
        screenHintText.gameObject.SetActive(true);
        screenHintText.text = text;
    }
}
