using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IActivateable : MonoBehaviour
{
    public bool isActivated;

    void Start()
    {
        Debug.LogError("IActivateable is deprecated. Please use Activatable instead on " + gameObject.name);
    }
}
