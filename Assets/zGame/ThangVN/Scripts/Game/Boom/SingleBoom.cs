using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBoom : IBoom
{
    public Transform energy;
    public Transform coin;

    public List<int> listRatio;
    public override void Init(Vector3 spawnPos)
    {
        base.Init(spawnPos);
        energy.gameObject.SetActive(false);
        coin.gameObject.SetActive(false);

        int rd = -1;

        int randomRatio = Random.Range(0, 100);
        for (int i = 0; i < listRatio.Count; i++)
        {
            if (listRatio[i] > randomRatio)
            {
                rd = i;
                break;
            }

            rd = listRatio.Count + 1;
        }

        if (rd == 0) energy.gameObject.SetActive(true);
        else if (rd == 1) coin.gameObject.SetActive(true);
        else
        {
            energy.gameObject.SetActive(false);
            coin.gameObject.SetActive(false);
        }
    }

    public override void SpawnParticle(Vector3 posParticle, Vector3 posItem)
    {
        base.SpawnParticle(posParticle, posItem);
        LogicGame.Instance.singleBoomPool.Spawn(posParticle, true);

        if (energy.gameObject.activeSelf)
        {
            Energy spawnedEnergy = CustomPoolController.Instance.GetEnergy();
            spawnedEnergy.transform.position = posItem;
            //LogicGame.Instance.listEnergy.Add(spawnedEnergy);
            spawnedEnergy.Init();
        }

        if (coin.gameObject.activeSelf)
        {
            Coin spawnedCoin = CustomPoolController.Instance.GetCoin();
            spawnedCoin.transform.position = posItem;
            //LogicGame.Instance.listCoin.Add(spawnedCoin);

            spawnedCoin.Init();
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
