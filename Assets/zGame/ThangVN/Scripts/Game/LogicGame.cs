using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms;
using Utilities.Common;

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
    public LogicPlayer player;
    [SerializeField] float timeSpawn = -1f;
    [SerializeField] List<ListSpawnPos> listContaineListSpawn;
    [SerializeField] CustomPoolController poolManager;
    [SerializeField] public float timerCount;
    [SerializeField] Transform holderParticles;

    public List<GameObject> listBoom;
    public List<LogicShadow> listShadow;
    public List<Energy> listEnergy;
    public List<Coin> listCoin;
    //[SerializeField] float timeSpawnBoomSpecial = -1f;

    public CustomPool<ParticleSystem> singleBoomPool;
    public CustomPool<ParticleSystem> bigBoomPool;
    public CustomPool<ParticleSystem> fireBoomPool;

    [SerializeField] private ParticleSystem singleBoomPrefab;
    [SerializeField] private ParticleSystem bigBoomPrefab;
    [SerializeField] private ParticleSystem fireBoomPrefab;
    [SerializeField] LogicDataSpawn logicData;
    public bool isUseEnergy;
    public bool isUseMagnet;

    public float timeUseMagnet;
    public float timeUseShield;


    public bool isUseShield;
    public bool isUseSBoom;
    [SerializeField] float timeSpawnItem = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timerCount = 0f;
        timeSpawnItem = 5f;
        singleBoomPool = new CustomPool<ParticleSystem>(singleBoomPrefab, 5, holderParticles, false);
        bigBoomPool = new CustomPool<ParticleSystem>(bigBoomPrefab, 5, holderParticles, false);
        fireBoomPool = new CustomPool<ParticleSystem>(fireBoomPrefab, 5, holderParticles, false);
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

        SpawnItem();
        CountDownUseItem();

    }

    private void CountDownUseItem()
    {
        if (timeUseShield > 0f)
        {
            timeUseShield -= Time.deltaTime;
            player.shield.SetActive(true);
        }
        else
        {
            timeUseShield = 0f;
            player.shield.SetActive(false);
        }

        if (timeUseMagnet > 0f)
        {
            timeUseMagnet -= Time.deltaTime;
            player.magnet.SetActive(true);
        }
        else
        {
            timeUseMagnet = 0f;
            player.magnet.SetActive(false);
        }
    }

    private void SpawnItem()
    {
        if (timeSpawnItem > 0f) timeSpawnItem -= Time.deltaTime;
        else
        {
            timeSpawnItem = 5f;
            Debug.Log("spawn item");
            int index = UnityEngine.Random.Range(2, 3);
            SelectItemSpawn(index);
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
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
            bigBoom.Init(spawnPos);
            listBoom.Add(bigBoom.gameObject);

            LogicShadow shadow = poolManager.GetShadow();
            Vector3 posShadow = new Vector3(spawnPos.x, -7f, 0);
            shadow.transform.position = posShadow;
            listShadow.Add(shadow);

            yield return new WaitForSeconds(duration / countBoom);
        }

    }

    IEnumerator SpawnSingleBoom(int index, float range, int countBoom, float duration)
    {
        for (int i = 0; i < countBoom; i++)
        {
            SingleBoom singleBoom = poolManager.GetSingleBoom();
            Vector3 spawnPos = new Vector3(listContaineListSpawn[index].startPos.position.x + range * i, listContaineListSpawn[index].startPos.position.y, 0);
            singleBoom.Init(spawnPos);
            listBoom.Add(singleBoom.gameObject);

            LogicShadow shadow = poolManager.GetShadow();
            Vector3 posShadow = new Vector3(spawnPos.x, -7f, 0);
            shadow.transform.position = posShadow;
            listShadow.Add(shadow);

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
                    singleBoom.Init(spawnPos);
                    listBoom.Add(singleBoom.gameObject);


                    LogicShadow shadow = poolManager.GetShadow();
                    Vector3 posShadow = new Vector3(spawnPos.x, -7f, 0);
                    shadow.transform.position = posShadow;
                    listShadow.Add(shadow);
                }

                //yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(duration / numberOfCouple);
        }
    }

    void SelectItemSpawn(int type)
    {
        Vector3 posSpawn = new Vector3(UnityEngine.Random.Range(-7f, 7f), 10f, 0);
        switch (type)
        {
            case 0:
                Magnet magnet = poolManager.GetMagnet();
                magnet.Init(posSpawn);
                break;
            case 1:
                Shield shield = poolManager.GetShield();
                shield.Init(posSpawn);
                break;
            case 2:
                SBoom sBoom = poolManager.GetSBoom();
                sBoom.Init(posSpawn);
                break;
            default:
                break;
        }
    }

    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    IEnumerator UseMagnet()
    {
        isUseMagnet = true;
        yield return new WaitForSeconds(7f);
        isUseMagnet = false;
    }




}
