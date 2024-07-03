using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;

    private void OnGUI()
    {
        if (GUILayout.Button("Clear Data"))
        {
            Clear();
        }

    }

    void Clear()
    {
        for (int i = 1; i < characterData.listCharacter.Count; i++)
        {
            CharacterData.Character charData = characterData.listCharacter.Find(x => x.index == i);
            if (charData != null)
            {
                charData.unlocked = false;
                charData.levelSkill = 0;
            }
        }
        SaveData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(new CharacterDataAll()
        {
            characters = characterData.listCharacter.ToArray(),
        }, true);
        PlayerPrefs.SetString(GameConfig.CHARACTERDATA, json);
        Debug.Log(" saveData" + json);

        PlayerPrefs.Save();
    }
}
