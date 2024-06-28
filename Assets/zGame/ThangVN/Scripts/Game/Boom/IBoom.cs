using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class IBoom : MonoBehaviour
{
    public float offSetSpeed = 5f;
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

    //if (transform != null)
    //{
    //    transform.DORotate(new Vector3(transform.localEulerAngles.x + 180f, transform.eulerAngles.y, transform.localEulerAngles.z), 0.2f, RotateMode.Fast);
    //}

    public virtual void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= 1f * offSetSpeed * Time.deltaTime;
        transform.position = newPosition;
        //transform.DOMoveY(transform.position.y - 0.1f * offSetSpeed, 0.1f * offSetSpeed);
    }
}
