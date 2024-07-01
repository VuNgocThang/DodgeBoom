using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnShake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        //transform.DOShakeRotation(duration, strength);
    }

   
}
