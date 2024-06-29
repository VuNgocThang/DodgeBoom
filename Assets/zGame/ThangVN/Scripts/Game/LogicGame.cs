using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float totalTimeInWave = 3f;
    [SerializeField] float offSet = 3f;
    [SerializeField] float offsetInPair = 1f;
    [SerializeField] float offsetBetweenPairs = 3f;
    int countDiff = 0;
    private IEnumerator SpawnSingleBoom()
    {
        isSpawning = true;

        int index = countDiff % 3;
        countDiff++;
        if (countDiff > 3) countDiff = 0;
        //List<Transform> spawnPositions = listContaineListSpawn[index].listPos;
        //int numberOfPairs = spawnPositions.Count / 2;

        int countAll = 20;
        int numberOfPairs = 20 / 2;

        //int count = listContaineListSpawn[index].listPos.Count;

        for (int i = 0; i < numberOfPairs; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int posIndex = i * 2 + j;
                //if (posIndex < spawnPositions.Count)
                if (posIndex < countAll)
                {
                    SingleBoom singleBoom = poolManager.GetSingleBoom();
                    float xPos = listContaineListSpawn[index].startPos.position.x + i * offsetBetweenPairs + j * offsetInPair;
                    Vector3 spawnPos = new Vector3(xPos, listContaineListSpawn[index].startPos.position.y, 0);
                    singleBoom.Init(spawnPos);
                }

                //yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(totalTimeInWave / numberOfPairs);
        }

        //if (spawnPositions.Count % 2 != 0)
        if (countAll % 2 != 0)
        {
            //int lastIndex = spawnPositions.Count - 1;
            int lastIndex = countAll - 1;
            SingleBoom singleBoom = poolManager.GetSingleBoom();
            Vector3 spawnPos = new Vector3(
                listContaineListSpawn[index].startPos.position.x + lastIndex * offSet,
                listContaineListSpawn[index].startPos.position.y,
                0
            );
            singleBoom.Init(spawnPos);
        }

        isSpawning = false;
    }

    private IEnumerator SpawnBoomBig()
    {
        int index = UnityEngine.Random.Range(0, listContaineListSpawn.Count);
        isSpawning = true;
        int count = listContaineListSpawn[index].listPos.Count;

        for (int i = 0; i < count; i++)
        {
            BigBoom bigBoom = poolManager.GetBigBoom();
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
            DoubleBoom doubleBoom = poolManager.GetDoubleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index2].startPos.position.x + i * offSet, listContaineListSpawn[index2].startPos.position.y, 0);
            doubleBoom.Init(spawnPos);

            yield return new WaitForSeconds(totalTimeInWave / count);
        }

        isSpawning = false;
    }
}
