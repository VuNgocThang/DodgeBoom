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

public enum TypeBoom
{
    singleBoom,
    doubleBoom,
    bigBoom,
}

public class LogicGame : MonoBehaviour
{
    public TypeBoom typeBoom;
    [SerializeField] float timeSpawn = -1f;
    [SerializeField] List<ListSpawnPos> listContaineListSpawn;
    [SerializeField] LogicPlayer player;
    [SerializeField] BoomManager boomManager;
    public List<IBoom> listBoom;

    //[SerializeField] float timeSpawnBoomSpecial = -1f;
    [SerializeField] public static float timerCount;
    [SerializeField] float totalTimeInWave = 3f;
    [SerializeField] float offSet = 3f;

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
                if (typeBoom == TypeBoom.singleBoom)
                {
                    StartCoroutine(SpawnSingleBoom());
                }
                else if (typeBoom == TypeBoom.doubleBoom)
                {
                    StartCoroutine(SpawnDoubleBoom());
                }
                else if (typeBoom == TypeBoom.bigBoom)
                {
                    StartCoroutine(SpawnBoomBig());
                }
                //if (timerCount < 20f)
                //{
                //}
                //else
                //{
                //    StartCoroutine(SpawnBoomBig());
                //}
            }
        }

        //if (timeSpawnBoomSpecial > 0f)
        //{
        //    timeSpawnBoomSpecial -= Ez.TimeMod;
        //}
        //else
        //{
        //    timeSpawnBoomSpecial = 2f;

        //    if (timerCount > 20f)
        //    {
        //        DoubleBoom doubleBoom = boomManager.GetDoubleBoom();
        //        Vector3 spawnPos = new Vector3(player.transform.position.x, 10f, 0f);
        //        doubleBoom.Init(spawnPos);
        //        listBoom.Add(doubleBoom);
        //    }

        //}
    }

    bool isSpawning;
    private IEnumerator SpawnSingleBoom()
    {
        int index1 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        isSpawning = true;
        int count = listContaineListSpawn[index1].listPos.Count;

        for (int i = 0; i < count; i++)
        {
            SingleBoom singleBoom = boomManager.GetSingleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index1].startPos.position.x + i * offSet, listContaineListSpawn[index1].startPos.position.y, 0);
            singleBoom.Init(spawnPos);

            yield return new WaitForSeconds(totalTimeInWave / count);
        }

        //int index2 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        //for (int i = 0; i < listContaineListSpawn[index2].listPos.Count; i++)
        //{
        //    DoubleBoom doubleBoom = boomManager.GetDoubleBoom();
        //    Vector3 spawnPos = new Vector3(listContaineListSpawn[index2].startPos.position.x + i * offSet, listContaineListSpawn[index2].startPos.position.y, 0);
        //    doubleBoom.Init(spawnPos);
        //    listBoom.Add(doubleBoom);

        //    yield return new WaitForSeconds(0.5f);
        //}

        isSpawning = false;
    }

    private IEnumerator SpawnBoomBig()
    {
        int index = UnityEngine.Random.Range(0, listContaineListSpawn.Count);
        isSpawning = true;
        int count = listContaineListSpawn[index].listPos.Count;

        for (int i = 0; i < count; i++)
        {
            BigBoom bigBoom = boomManager.GetBigBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index].startPos.position.x + i * offSet, listContaineListSpawn[index].startPos.position.y, 0);
            bigBoom.Init(spawnPos);
            yield return new WaitForSeconds(totalTimeInWave / count);
        }
        isSpawning = false;
    }

    private IEnumerator SpawnDoubleBoom()
    {
        int index1 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        isSpawning = true;
        int count = listContaineListSpawn[index1].listPos.Count;

        int index2 = UnityEngine.Random.Range(0, listContaineListSpawn.Count);

        for (int i = 0; i < listContaineListSpawn[index2].listPos.Count; i++)
        {
            DoubleBoom doubleBoom = boomManager.GetDoubleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index2].startPos.position.x + i * offSet, listContaineListSpawn[index2].startPos.position.y, 0);
            doubleBoom.Init(spawnPos);

            yield return new WaitForSeconds(totalTimeInWave / count);
        }

        isSpawning = false;
    }

}
