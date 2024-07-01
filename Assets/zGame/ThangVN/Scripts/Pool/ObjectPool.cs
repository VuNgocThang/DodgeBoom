using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> pool;
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = GameObject.Instantiate(prefab, parent);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}


