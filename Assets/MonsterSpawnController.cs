using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;
namespace Dev_ChickenMerge
{
    [System.Serializable]
    public class DataToolEnemy
    {
        public NameEnemy nameEnemy;
        public float timeSpawn;
    }
    [System.Serializable]
    public class ListDataToolEnemy
    {
        public List<DataToolEnemy> dataToolEnemies = new List<DataToolEnemy>();
    }

    public class MonsterSpawnController : PoolManager
    {
        public static MonsterSpawnController ins;
        public Transform pointPool;
        public List<Enemy> listEnemy = new List<Enemy>();
        public List<Enemy> listEnemyPref = new List<Enemy>();
        public List<GameObject> listPointSpawn = new List<GameObject>();
        List<Vector3> listPoint = new List<Vector3>();
        public bool isSetTool;
        public bool isStartGame;
        private void Awake()
        {
            ins = this;
            RandomPointSpawnEnemy();
        }
        private void Start()
        {
            for (int i = 0; i < listEnemyPref.Count; i++)
            {
                SetUpInit(listEnemyPref[i].gameObject, 5, pointPool);
            }
        }
        public GameObject GetEnemy(int id)
        {
            return GetPollObject(listEnemyPref[id].gameObject);
        }

        public void AddToListEnemy(GameObject obj)
        {
            Enemy enemy_ = obj.GetComponent<Enemy>();
            listEnemy.Add(enemy_);
        }
        public void RemoveEnemy(Enemy enemy_)
        {
            listEnemy.Remove(enemy_);
        }
        void RandomPointSpawnEnemy()
        {
            for (int i = 0; i < listPointSpawn.Count; i++)
            {
                listPoint.Add(listPointSpawn[i].transform.localPosition);
            }
            listPoint.Shuffle();
            for (int i = 0; i < listPointSpawn.Count; i++)
            {
                listPointSpawn[i].transform.localPosition = listPoint[i];
            }
        }
        public Vector3 PointStart()
        {
            return new Vector3(20, 0, 0);
        }
    }
}
