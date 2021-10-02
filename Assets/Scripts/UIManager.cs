
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Ping")]
    public Image pingDisplayImage;
    public Image nextPingImage;
    public Text pingValue;
    public Text nextPingValue;
    private float pingNotifyTimeout = 0;

    [Header("Hints")]
    public GameObject hintPanel;
    public Text hintText;

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
    }

    void Update()
    {
        if (PingController.Instance.pingQueue.Count == 0) return;
        pingDisplayImage.color = PingController.Instance.lag.color;
        if (PingController.Instance.nextLag != null)
        {
            nextPingImage.color = PingController.Instance.nextLag.color;
        }

        if (Time.time >= pingNotifyTimeout)
        {
            pingNotifyTimeout += PingController.Instance.realTimePingInterval;
            pingValue.text = ((int)Mathf.Round(PingController.Instance.realtimePing)).ToString() + " ms";
            nextPingValue.text = PingController.Instance.nextLag.delay.ToString() + " ms";
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
}
