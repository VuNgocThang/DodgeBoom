using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoom : IBoom
{
    public override void Init(Vector3 spawnPos)
    {
        base.Init(spawnPos);
    }

    public override void SpawnParticle(Vector3 posParticle, Vector3 posItem)
    {
        base.SpawnParticle(posParticle, posItem);
        LogicGame.Instance.bigBoomPool.Spawn(posParticle, true);
        CameraShake.Instance.OnShake(0.2f, 1);
    }

    public override void Update()
    {
        base.Update();
    }

}
