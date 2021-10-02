
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image pingDisplayImage;
    public Image nextPingImage;

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
    }
}
