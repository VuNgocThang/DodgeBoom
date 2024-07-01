using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms;


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
    [SerializeField] public float timerCount;

    public List<GameObject> listBoom;
    //[SerializeField] float timeSpawnBoomSpecial = -1f;

    public CustomPool<ParticleSystem> singleBoomPool;
    public CustomPool<ParticleSystem> bigBoomPool;
    public CustomPool<ParticleSystem> fireBoomPool;

    [SerializeField] private ParticleSystem singleBoomPrefab;
    [SerializeField] private ParticleSystem bigBoomPrefab;
    [SerializeField] private ParticleSystem fireBoomPrefab;
    [SerializeField] LogicDataSpawn logicData;
    public bool isUseEnergy;

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
        SaveGame.Energy = 0;
    }

    private void Update()
    {
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

        timerCount += Time.deltaTime;
        timeCanReset += Time.deltaTime;

        if (timeIsSpawning > 0f)
        {
            timeIsSpawning -= Time.deltaTime;
        }
        else
        {
            if (timeCanReset > 52f) timeCanReset = 0f;

            for (int i = logicData.listTimeData.Count - 1; i >= 0; i--)
            {
                if (timeCanReset >= logicData.listTimeData[i].time)
                {
                    timeIsSpawning = logicData.listTimeData[i].duration;
                    int type = logicData.listTimeData[i].listTypeBooms[0];
                    float range = logicData.listTimeData[i].range;
                    int countBoom = logicData.listTimeData[i].countBoom;
                    float duration = logicData.listTimeData[i].duration;

                    SpawnBoom(type, range, countBoom, duration);
                    break;
                }
            }
        }
    }

    float timeCanReset = 0f;


    public float timeIsSpawning = 0f;
    public int countDiff = 0;

    private void SpawnBoom(int type, float range, int countBoom, float duration)
    {
        int index = countDiff % 3;
        countDiff++;
        if (countDiff > 3) countDiff = 0;

        SelectBoomSpawn(type, index, range, countBoom, duration);
    }

    void SelectBoomSpawn(int type, int index, float range, int countBoom, float duration)
    {
        switch (type)
        {
            case 0:
                StartCoroutine(SpawnSingleBoom(index, range, countBoom, duration));
                break;
            case 1:
                StartCoroutine(SpawnCoupleBoom(index, range, countBoom, duration, 2));
                break;
            case 2:
                StartCoroutine(SpawnBigBoom(index, range, countBoom, duration));
                break;
            case 3:
                StartCoroutine(SpawnCoupleBoom(index, range, countBoom, duration, 3));
                break;
            case 4:
                StartCoroutine(SpawnCoupleBoom(index, range, countBoom, duration, 4));
                break;
            default:
                break;
        }
    }

    IEnumerator SpawnBigBoom(int index, float range, int countBoom, float duration)
    {
        for (int i = 0; i < countBoom; i++)
        {
            BigBoom bigBoom = poolManager.GetBigBoom();
            Vector3 spawnPosBigBoom = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
            bigBoom.Init(spawnPosBigBoom);
            listBoom.Add(bigBoom.gameObject);

            yield return new WaitForSeconds(duration / countBoom);
        }

    }

    IEnumerator SpawnSingleBoom(int index, float range, int countBoom, float duration)
    {
        for (int i = 0; i < countBoom; i++)
        {
            SingleBoom singleBoom = poolManager.GetSingleBoom();
            Vector3 spawnPosSingleBoom = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
            singleBoom.Init(spawnPosSingleBoom);
            listBoom.Add(singleBoom.gameObject);
            yield return new WaitForSeconds(duration / countBoom);
        }
    }

    private IEnumerator SpawnCoupleBoom(int index, float range, int countBoom, float duration, int couple)
    {
        int numberOfCouple = countBoom / couple;
        for (int x = 0; x < numberOfCouple; x++)
        {
            for (int j = 0; j < couple; j++)
            {
                int posIndex = x * couple + j;
                if (posIndex < countBoom)
                {
                    SingleBoom singleBoom = poolManager.GetSingleBoom();
                    float xPos = listContaineListSpawn[index].startPos.position.x + x * range + j * 1f;
                    Vector3 spawnPos = new Vector3(xPos, listContaineListSpawn[index].startPos.position.y, 0);
                    listBoom.Add(singleBoom.gameObject);

                    singleBoom.Init(spawnPos);
                }

                //yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(duration / numberOfCouple);
        }
    }

    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}
