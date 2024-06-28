using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Pool
{
    public GameObject poolPrefab;
    public List<GameObject> listPoolObject = new List<GameObject>();
    public Transform parentSpawn;

}
public class CustomPoolManager : MonoBehaviour
{
    public List<Pool> listPool = new List<Pool>();
    public void SetUpInit(GameObject prefab, int amount, Transform parentSpawn = null)
    {
        Pool pool = new Pool();
        pool.poolPrefab = prefab;
        pool.parentSpawn = parentSpawn;
        for (int i = 0; i < amount; i++)
        {
            GameObject objPoll;
            if (parentSpawn == null)
            {
                objPoll = Instantiate(prefab);
            }
            else
            {
                objPoll = Instantiate(prefab, parentSpawn);
            }
            //objPoll.transform.localPosition= Vector3.zero;
            objPoll.transform.localPosition = new Vector3(20, 0, 0);
            objPoll.SetActive(false);
            pool.listPoolObject.Add(objPoll);
        }
        listPool.Add(pool);
    }
    public GameObject GetPollObject(GameObject prefab)
    {
        for (int i = 0; i < listPool.Count; i++)
        {
            if (listPool[i].poolPrefab == prefab)
            {
                for (int j = 0; j < listPool[i].listPoolObject.Count; j++)
                {
                    if (!listPool[i].listPoolObject[j].activeInHierarchy)
                    {
                        return listPool[i].listPoolObject[j];
                    }
                }
                GameObject objPool = Instantiate(prefab, listPool[i].parentSpawn);
                objPool.SetActive(false);
                listPool[i].listPoolObject.Add(objPool);
                return objPool;
            }
        }
        return null;
    }
}