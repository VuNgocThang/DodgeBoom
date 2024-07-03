using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : IItems
{
    public override void Init(Vector3 spawnPos)
    {
        base.Init(spawnPos);
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("useShield");
        LogicGame.Instance.timeUseShield = 7f;
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
