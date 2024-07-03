using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : IItems
{
    public override void Init(Vector3 spawnPos)
    {
        base.Init(spawnPos);
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("use Magnet");
        gameObject.SetActive(false);
        LogicGame.Instance.timeUseMagnet = 7f;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (LogicGame.Instance.IsInLayerMask(other.gameObject, layerPlayer))
        {
            Execute();
        }
    }

}
