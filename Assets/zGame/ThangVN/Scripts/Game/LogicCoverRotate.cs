using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicCoverRotate : MonoBehaviour
{
    public Transform center;
    public float orbitSpeed = 10f; 
    public float orbitRadius = 5f; 

    private float angle;

    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;

        if (angle >= 360f) angle -= 360f;

        float posX = center.position.x + Mathf.Cos(angle) * orbitRadius;
        float posY = center.position.y + Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
