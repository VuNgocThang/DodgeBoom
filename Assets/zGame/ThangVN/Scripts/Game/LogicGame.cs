using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

[Serializable]
public class ListSpawnPos
{
    public List<Transform> listPos;

    public Transform startPos;
}

public class LogicGame : MonoBehaviour
{
    [SerializeField] float timeSpawn = -1f;
    [SerializeField] List<ListSpawnPos> listContaineListSpawn;
    [SerializeField] LogicPlayer player;
    [SerializeField] BoomManager boomManager;
    public List<IBoom> listBoom;

    [SerializeField] float timeSpawnBoomSpecial = -1f;
    [SerializeField] public static float timerCount;

    private void Start()
    {
        timerCount = 0f;
    }

    private void Update()
    {
        timerCount += Ez.TimeMod;

        if (timeSpawn > 0f)
        {
            timeSpawn -= Ez.TimeMod;
        }
        else
        {
            timeSpawn = 1.5f;

            if (!isSpawning)
            {
                if (timerCount < 20f)
                {
                    StartCoroutine(SpawnBooms());
                }
                else
                {
                    StartCoroutine(SpawnBoomBig());
                }
            }
        }

        if (timeSpawnBoomSpecial > 0f)
        {
            timeSpawnBoomSpecial -= Ez.TimeMod;
        }
        else
        {
            timeSpawnBoomSpecial = 2f;

            if (timerCount > 20f)
            {
                DoubleBoom doubleBoom = boomManager.GetDoubleBoom();
                Vector3 spawnPos = new Vector3(player.transform.position.x, 10f, 0f);
                doubleBoom.Init(spawnPos);
                listBoom.Add(doubleBoom);
            }

        }
    }

    bool isSpawning;
    private IEnumerator SpawnBooms()
    {
        int index1 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        isSpawning = true;

        for (int i = 0; i < listContaineListSpawn[index1].listPos.Count; i++)
        {
            SingleBoom singleBoom = boomManager.GetSingleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index1].listPos[0].position.x + i * 3f, listContaineListSpawn[index1].listPos[0].position.y, 0);
            singleBoom.Init(spawnPos);
            listBoom.Add(singleBoom);

            yield return new WaitForSeconds(0.3f);
        }

        int index2 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        for (int i = 0; i < listContaineListSpawn[index2].listPos.Count; i++)
        {
            DoubleBoom doubleBoom = boomManager.GetDoubleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index2].listPos[0].position.x + i * 3f, listContaineListSpawn[index2].listPos[0].position.y, 0);
            //Vector3 spawnPos = listContaineListSpawn[index2].listPos[i].position;
            doubleBoom.Init(spawnPos);
            listBoom.Add(doubleBoom);

            yield return new WaitForSeconds(0.5f);
        }

        isSpawning = false;
    }

    private IEnumerator SpawnBoomBig()
    {
        int index = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        isSpawning = true;
        for (int i = 0; i < listContaineListSpawn[index].listPos.Count; i++)
        {
            BigBoom bigBoom = boomManager.GetBigBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index].listPos[0].position.x + i * 3f, listContaineListSpawn[index].listPos[0].position.y, 0);
            bigBoom.Init(spawnPos);
            listBoom.Add(bigBoom);

            yield return new WaitForSeconds(0.7f);
        }
        isSpawning = false;
    }



}
