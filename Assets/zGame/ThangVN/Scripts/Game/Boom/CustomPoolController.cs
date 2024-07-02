using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPoolController : MonoBehaviour
{
    public static CustomPoolController Instance;
    public Transform parent;

    public SingleBoom singleBoomPrefab;
    public BigBoom bigBoomPrefab;
    public Coin coinPrefab;
    public Energy energyPrefab;
    public LogicShadow shadownPrefab;

    private ObjectPool<SingleBoom> singleBoomPool;
    private ObjectPool<BigBoom> bigBoomPool;
    private ObjectPool<Coin> coinPool;
    private ObjectPool<Energy> energyPool;
    private ObjectPool<LogicShadow> shadowPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        singleBoomPool = new ObjectPool<SingleBoom>(singleBoomPrefab, 10, parent);
        bigBoomPool = new ObjectPool<BigBoom>(bigBoomPrefab, 10, parent);
        coinPool = new ObjectPool<Coin>(coinPrefab, 10, parent);
        energyPool = new ObjectPool<Energy>(energyPrefab, 10, parent);
        shadowPool = new ObjectPool<LogicShadow>(shadownPrefab, 10, parent);
    }

    public SingleBoom GetSingleBoom()
    {
        return singleBoomPool.GetObject();
    }

    public BigBoom GetBigBoom()
    {
        return bigBoomPool.GetObject();
    }

    public Coin GetCoin()
    {
        return coinPool.GetObject();
    }

    public Energy GetEnergy()
    {
        return energyPool.GetObject();
    }
    public LogicShadow GetShadow()
    {
        return shadowPool.GetObject();
    }

    public void ReturnSingleBoom(SingleBoom singleBoom)
    {
        singleBoomPool.ReturnObject(singleBoom);
    }

    public void ReturnBigBoom(BigBoom bigBoom)
    {
        bigBoomPool.ReturnObject(bigBoom);
    }

    public void ReturnCoin(Coin coin)
    {
        coinPool.ReturnObject(coin);
    }

    public void ReturnEnergy(Energy energy)
    {
        energyPool.ReturnObject(energy);
    }
    public void ReturnShadow(LogicShadow shadow)
    {
        shadowPool.ReturnObject(shadow);
    }
}
