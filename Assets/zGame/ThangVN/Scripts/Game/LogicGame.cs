using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


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
    public static LogicGame Instance;
    public TypeBoom typeBoom;
    [SerializeField] float timeSpawn = -1f;
    [SerializeField] List<ListSpawnPos> listContaineListSpawn;
    [SerializeField] LogicPlayer player;
    [SerializeField] CustomPoolController poolManager;
    public List<IBoom> listBoom;

    //[SerializeField] float timeSpawnBoomSpecial = -1f;
    [SerializeField] public static float timerCount;

    public CustomPool<ParticleSystem> singleBoomPool;
    public CustomPool<ParticleSystem> bigBoomPool;
    public CustomPool<ParticleSystem> fireBoomPool;

    [SerializeField] private ParticleSystem singleBoomPrefab;
    [SerializeField] private ParticleSystem bigBoomPrefab;
    [SerializeField] private ParticleSystem fireBoomPrefab;
    [SerializeField] LogicDataSpawn logicData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timerCount = 0f;
        singleBoomPool = new CustomPool<ParticleSystem>(singleBoomPrefab, 5, transform, false);
        bigBoomPool = new CustomPool<ParticleSystem>(bigBoomPrefab, 5, transform, false);
        fireBoomPool = new CustomPool<ParticleSystem>(fireBoomPrefab, 5, transform, false);
    }

    private void Update()
    {
        timerCount += Ez.TimeMod;

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

        timerCount += Ez.TimeMod;
        Debug.Log("timeCount: " + timerCount);

        if (timeIsSpawning > 0f)
        {
            timeIsSpawning -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < logicData.listTimeData.Count; i++)
            {
                if (timerCount < logicData.listTimeData[i].time)
                {
                    timeIsSpawning = logicData.listTimeData[i].duration;
                    StartCoroutine(SpawnBoom(logicData.listTimeData[i].listTypeBooms[0], logicData.listTimeData[i].range, logicData.listTimeData[i].duration));
                    break;
                }
            }
        }
    }


    float timeIsSpawning = 0f;
    int countDiff = 0;

    private IEnumerator SpawnBoom(int type, float range, float duration)
    {
        int index = countDiff % 3;
        countDiff++;
        if (countDiff > 3) countDiff = 0;

        for (int i = 0; i < 10; i++)
        {
            Debug.Log("typeBoom:" + type);
            SelectBoomSpawn(type, index, range, i);
            yield return new WaitForSeconds(duration / 10);
        }
    }

    void SelectBoomSpawn(int type, int index, float range, int i)
    {
        switch (type)
        {
            case 0:
                SingleBoom singleBoom = poolManager.GetSingleBoom();
                Vector3 spawnPosSingleBoom = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
                singleBoom.Init(spawnPosSingleBoom);
                break;
            case 1:
                BigBoom bigBoom = poolManager.GetBigBoom();
                Vector3 spawnPosBigBoom = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
                bigBoom.Init(spawnPosBigBoom);
                break;
            default:
                break;
        }
    }

    //private IEnumerator SpawnDoubleBoom()
    //{
    //    isSpawning = true;
    //    int index = countDiff % 3;
    //    countDiff++;
    //    if (countDiff > 3) countDiff = 0;
    //    //List<Transform> spawnPositions = listContaineListSpawn[index].listPos;
    //    //int numberOfPairs = spawnPositions.Count / 2;

    //    int countAll = 20;
    //    int numberOfPairs = 20 / 2;

    //    //int count = listContaineListSpawn[index].listPos.Count;

    //    for (int i = 0; i < numberOfPairs; i++)
    //    {
    //        for (int j = 0; j < 2; j++)
    //        {
    //            int posIndex = i * 2 + j;
    //            //if (posIndex < spawnPositions.Count)
    //            if (posIndex < countAll)
    //            {
    //                SingleBoom singleBoom = poolManager.GetSingleBoom();
    //                float xPos = listContaineListSpawn[index].startPos.position.x + i * offsetBetweenPairs + j * offsetInPair;
    //                Vector3 spawnPos = new Vector3(xPos, listContaineListSpawn[index].startPos.position.y, 0);
    //                singleBoom.Init(spawnPos);
    //            }

    //            //yield return new WaitForSeconds(0.1f);
    //        }
    //        yield return new WaitForSeconds(totalTimeInWave / numberOfPairs);
    //    }

    //    //if (spawnPositions.Count % 2 != 0)
    //    if (countAll % 2 != 0)
    //    {
    //        //int lastIndex = spawnPositions.Count - 1;
    //        int lastIndex = countAll - 1;
    //        SingleBoom singleBoom = poolManager.GetSingleBoom();
    //        Vector3 spawnPos = new Vector3(
    //            listContaineListSpawn[index].startPos.position.x + lastIndex * offSet,
    //            listContaineListSpawn[index].startPos.position.y,
    //            0
    //        );
    //        singleBoom.Init(spawnPos);
    //    }

    //    isSpawning = false;
    //}
}
