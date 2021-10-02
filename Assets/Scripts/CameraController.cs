using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.075f;
    public bool isFollowingTarget = true;

    void Start()
    {

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void FixedUpdate()
    {
        if (!target) return;
        if (!isFollowingTarget) return;
        Vector2 targetPosition = target.position;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }
}
