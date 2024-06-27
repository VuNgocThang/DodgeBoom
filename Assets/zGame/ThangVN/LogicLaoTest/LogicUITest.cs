using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicUITest : MonoBehaviour
{
    public TextMeshProUGUI txtTime;

    private void Update()
    {
        int m = Mathf.RoundToInt(LogicGame.timerCount / 60);
        int s = Mathf.RoundToInt(LogicGame.timerCount % 60);
        txtTime.text = $"{m} : {s}";
    }
}
