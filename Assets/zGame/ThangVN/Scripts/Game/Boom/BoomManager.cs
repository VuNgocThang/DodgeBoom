using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomManager : MonoBehaviour
{
    public SingleBoom singleBoomPrefab;
    public DoubleBoom doubleBoomPrefab;
    public BigBoom bigBoomPrefab;

    private ObjectPool<SingleBoom> singleBoomPool;
    private ObjectPool<DoubleBoom> doubleBoomPool;
    private ObjectPool<BigBoom> bigBoomPool;

    void Start()
    {
        singleBoomPool = new ObjectPool<SingleBoom>(singleBoomPrefab, 10);
        doubleBoomPool = new ObjectPool<DoubleBoom>(doubleBoomPrefab, 10);
        bigBoomPool = new ObjectPool<BigBoom>(bigBoomPrefab, 10);
    }

    public SingleBoom GetSingleBoom()
    {
        return singleBoomPool.GetObject();
    }

    public DoubleBoom GetDoubleBoom()
    {
        return doubleBoomPool.GetObject();
    }

    public BigBoom GetBigBoom()
    {
        return bigBoomPool.GetObject();
    }

    public void ReturnSingleBoom(SingleBoom singleBoom)
    {
        singleBoomPool.ReturnObject(singleBoom);
    }

    public void ReturnDoubleBoom(DoubleBoom doubleBoom)
    {
        doubleBoomPool.ReturnObject(doubleBoom);
    }

    public void ReturnBigBoom(BigBoom bigBoom)
    {
        bigBoomPool.ReturnObject(bigBoom);
    }
}
