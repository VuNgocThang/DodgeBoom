using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_ChickenMerge
{
    public class PoolingObj : CustomPoolManager
    {
        public static PoolingObj ins;
        public List<GameObject> listProjectile = new List<GameObject>();
        public List<GameObject> listExplosion = new List<GameObject>();
        public List<GameObject> listFlash = new List<GameObject>();
        private void Awake()
        {
            ins = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            SpawnProjectile();
            SpawnExplosion();
            SpawnFlash();
        }
        public GameObject GetProjectile(int id)
        {
            return GetPollObject(listProjectile[id]);
        }
        void SpawnProjectile()
        {
            for (int i = 0; i < listProjectile.Count; i++)
            {
                SetUpInit(listProjectile[i], 5, transform);
            }
        }

        void SpawnExplosion()
        {
            for (int i = 0; i < listExplosion.Count; i++)
            {
                SetUpInit(listExplosion[i], 5, transform);
            }
        }
        public GameObject GetExplosion(int id)
        {
            return GetPollObject(listExplosion[id]);
        }

        void SpawnFlash()
        {
            for (int i = 0; i < listFlash.Count; i++)
            {
                SetUpInit(listFlash[i], 5, transform);
            }
        }
        public GameObject GetFlash(int id)
        {
            return GetPollObject(listFlash[id]);
        }
    }
}
