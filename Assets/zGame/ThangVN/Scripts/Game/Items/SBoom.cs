using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoom : IItems
{
    public override void Init(Vector3 spawnPos)
    {
        base.Init(spawnPos);
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("use sBoom");
        foreach (GameObject boom in LogicGame.Instance.listBoom)
        {
            LogicGame.Instance.bigBoomPool.Spawn(boom.transform.position, true);
            boom.SetActive(false);
        }
        foreach (LogicShadow shadow in LogicGame.Instance.listShadow)
        {
            shadow.gameObject.SetActive(false);
        }

        LogicGame.Instance.listBoom.Clear();
        LogicGame.Instance.listShadow.Clear();
        gameObject.SetActive(false);
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
