using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Dev_ChickenMerge
{
    public class PointSpawnMonster : MonoBehaviour
    {
        public int index;
        int countList;
        float timeSpawn;
        bool isEndEnemy;
        public ListDataToolEnemy listDataToolEnemy = new ListDataToolEnemy();
        // Start is called before the first frame update
        void Start()
        {
            LoadDataEnemy();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) //SetTool
            {
                if (MonsterSpawnController.ins.isSetTool)
                {
                    SaveDataEnemy();
                }
            }
            UpgradeTimeSpawnEnemy();
        }
        public void UpgradeTimeSpawnEnemy()
        {
            if (CheckSpawnEnemy())
            {
                timeSpawn -= Time.deltaTime;
                if (timeSpawn <= 0)
                {
                    SpawnEnemy();
                    countList++;
                    if (CheckEndEnemy())
                    {
                        isEndEnemy = true;
                        Debug.Log("end Enemy");
                    }
                    AddTimeSpawn();
                }
            }
        }
        void AddTimeSpawn()
        {
            if (CheckSpawnEnemy())
            {
                timeSpawn = listDataToolEnemy.dataToolEnemies[countList].timeSpawn;
            }
        }
        void SpawnEnemy()
        {
            int id_ = GetIdEnemy();
            GameObject a_ = MonsterSpawnController.ins.GetEnemy(id_);
            a_.SetActive(true);
            MonsterSpawnController.ins.AddToListEnemy(a_);
            float posZ = (MonsterSpawnController.ins.listEnemy.Count / 100f);
            a_.transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
        }
        bool CheckSpawnEnemy()
        {
            if (MonsterSpawnController.ins.isStartGame && MonsterSpawnController.ins.isSetTool == false)
            {
                if (listDataToolEnemy.dataToolEnemies.Count > 0 && listDataToolEnemy.dataToolEnemies != null)
                {
                    if (isEndEnemy == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool CheckEndEnemy()
        {
            if (countList >= listDataToolEnemy.dataToolEnemies.Count)
            {
                return true;
            }
            return false;
        }
        int GetIdEnemy()
        {
            int id_ = 0;
            for (int i = 0; i < MonsterSpawnController.ins.listEnemyPref.Count; i++)
            {
                if (MonsterSpawnController.ins.listEnemyPref[i].nameEnemy == listDataToolEnemy.dataToolEnemies[countList].nameEnemy)
                {
                    id_ = i;
                }
            }
            return id_;
        }
        public void SaveDataEnemy()
        {
            //int lv_ = GamePlayController.ins.levelCurrent;
            int lv_ = 0;
            string json = JsonUtility.ToJson(listDataToolEnemy, true);
            File.WriteAllText(Application.dataPath + "/Resources/DataEnemy/Level_" + lv_ + "_"+ index + ".json", json);
            Debug.Log("Save Data -------- ");
        }
        public void LoadDataEnemy()
        {
            int lv_ = 0;
            //int lv_ = GamePlayController.ins.levelCurrent;
#if UNITY_EDITOR
            if (File.Exists(Application.dataPath + "/Resources/DataEnemy/Level_" + lv_ + "_" + index + ".json"))
            {
                string json_1 = File.ReadAllText(Application.dataPath + "/Resources/DataEnemy/Level_" + lv_ + "_" + index + ".json");
                listDataToolEnemy = JsonUtility.FromJson<ListDataToolEnemy>(json_1);
                AddTimeSpawn();
            }
#elif UNITY_ANDROID
            string json = Resources.Load<TextAsset>("DataEnemy/Level_" + lv_ + "_" + index).ToString();
            listDataToolEnemy = JsonUtility.FromJson<ListDataToolEnemy>(json);
            AddTimeSpawn();
#endif
        }
    }
}
