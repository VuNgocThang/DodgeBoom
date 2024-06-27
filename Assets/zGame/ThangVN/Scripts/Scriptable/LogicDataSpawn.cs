using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimeData
{
    public float time;
    public List<int> listTypeBooms;
    public float timeSpawnInList;
    public float timeSpawnEachList;
}


[CreateAssetMenu(fileName = "LogicDataSpawn", menuName = "ScriptableObjects/LogicDataSpawn")]
public class LogicDataSpawn : ScriptableObject
{
    public List<TimeData> listTimeData;
}
