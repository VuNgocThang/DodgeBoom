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
        offSetSpeed = 5f + (LogicGame.Instance.timerCount / 30f);
    }
    public virtual void SpawnParticle(Vector3 posParticle, Vector3 posItem)
    {

    }

    public virtual void Update()
    {
        if (LogicGame.Instance.isPauseGame) return;

        Vector3 newPosition = transform.position;
        newPosition.y -= 1f * offSetSpeed * Time.deltaTime;
        transform.position = newPosition;

        //if (transform != null)
        //{
        //    transform.DORotate(new Vector3(transform.localEulerAngles.x + 180f, transform.eulerAngles.y, transform.localEulerAngles.z), 0.2f, RotateMode.Fast);
        //}
        //transform.DOMoveY(transform.position.y - 0.1f * offSetSpeed, 0.1f * offSetSpeed);
    }
}
