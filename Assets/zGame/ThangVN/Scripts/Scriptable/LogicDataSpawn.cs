using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimeData
{
    public float time;
    public List<int> listTypeBooms;
    public float range;
    public float duration;
    public int countBoom;
}


[CreateAssetMenu(fileName = "LogicDataSpawn", menuName = "ScriptableObjects/LogicDataSpawn")]
public class LogicDataSpawn : ScriptableObject
{
    public List<TimeData> listTimeData;
}
