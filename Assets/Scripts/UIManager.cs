
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image pingDisplayImage;

    void Start()
    {

    }


    void Update()
    {
        pingDisplayImage.color = PingController.Instance.lag.color;
    }
}
