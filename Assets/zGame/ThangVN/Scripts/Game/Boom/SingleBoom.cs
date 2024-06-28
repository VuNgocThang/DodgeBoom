using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBoom : IBoom
{
    public Transform energy;
    public Transform coin;

    public List<int> listRatio = new List<int>()
    {
        20,
        40
    };
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

    public override void Update()
    {
        base.Update();
    }
}
