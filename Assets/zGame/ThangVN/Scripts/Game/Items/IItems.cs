using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IItems : MonoBehaviour
{
    public float offSetSpeed = 3f;
    public LayerMask layerPlayer;
    public virtual void Start()
    {

    }

    public virtual void Init(Vector3 spawnPos)
    {
        transform.position = spawnPos;
    }

    public virtual void Execute()
    {

    }

    public virtual void Update()
    {
        Vector3 newPosition = transform.position;
        if (newPosition.y > -6.5f)
        {
            newPosition.y -= 1f * offSetSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }
}
