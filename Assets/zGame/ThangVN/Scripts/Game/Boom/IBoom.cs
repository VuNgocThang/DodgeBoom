using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class IBoom : MonoBehaviour
{
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
    
}
