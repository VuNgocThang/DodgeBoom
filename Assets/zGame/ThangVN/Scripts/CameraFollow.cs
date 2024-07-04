using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offSet = new Vector3(0f, 7f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;

    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        Debug.Log(target.name);
        if (target == null) return;

        Vector3 targetPosition = target.position + offSet;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        smoothPosition.x = Mathf.Clamp(smoothPosition.x, minX, maxX);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, 0, 0);

        transform.position = smoothPosition;
    }
}
