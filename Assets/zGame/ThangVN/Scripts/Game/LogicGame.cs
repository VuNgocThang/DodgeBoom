using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;

[Serializable]
public class ListSpawnPos
{
    public List<Transform> listPos;
}

public class LogicGame : MonoBehaviour
{
    [SerializeField] LogicBoom prefabBoom;
    [SerializeField] float timeSpawn = 3f;
    [SerializeField] List<LogicBoom> listBooms;
    [SerializeField] List<ListSpawnPos> listContaineListSpawn;
    [SerializeField] LogicPlayer player;

    [SerializeField] float timeSpawnBoomSpecial = 2f;

    private void Update()
    {
        if (timeSpawn > 0f)
        {
            timeSpawn -= Ez.TimeMod;
        }
        else
        {
            timeSpawn = 2f;

            if (!isSpawning)
            {
                StartCoroutine(SpawnBooms());
            }
        }

        if (timeSpawnBoomSpecial > 0f)
        {
            timeSpawnBoomSpecial -= Ez.TimeMod;
        }
        else
        {
            timeSpawnBoomSpecial = 2f;
            LogicBoom boom = ObjectBoomPool.Instance.GetPooledObject();
            boom.gameObject.SetActive(true);
            boom.gameObject.name = $"boom special";
            Vector3 spawnPos = new  Vector3(player.transform.position.x, 10f, 0f);
            boom.Init(spawnPos);
        }
    }

    bool isSpawning;
    private IEnumerator SpawnBooms()
    {
        int index = UnityEngine.Random.Range(0, listContaineListSpawn.Count);
        isSpawning = true;
        for (int i = 0; i < listContaineListSpawn[index].listPos.Count; i++)
        {
            LogicBoom boom = ObjectBoomPool.Instance.GetPooledObject();
            boom.gameObject.SetActive(true);
            boom.gameObject.name = $"boom {i}";
            boom.Init(listContaineListSpawn[index].listPos[i].position);

            yield return new WaitForSeconds(0.3f);
        }
        isSpawning = false;
    }
}
