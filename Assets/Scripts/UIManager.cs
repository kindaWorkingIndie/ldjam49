
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image pingDisplayImage;
    public Image nextPingImage;

    public Text pingValue;
    public Text nextPingValue;
    void Start()
    {

    }


    void Update()
    {
        if (PingController.Instance.pingQueue.Count == 0) return;
        pingDisplayImage.color = PingController.Instance.lag.color;
        if (PingController.Instance.nextLag != null)
        {
            nextPingImage.color = PingController.Instance.nextLag.color;
        }

        pingValue.text = ((int)Mathf.Round(PingController.Instance.realtimePing)).ToString() + " ms";
        nextPingValue.text = PingController.Instance.nextLag.delay.ToString() + " ms";
    }
}
