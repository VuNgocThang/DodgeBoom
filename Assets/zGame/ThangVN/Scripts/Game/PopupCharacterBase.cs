using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PopupCharacterBase : Popup
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected int index = 0;
    [SerializeField] protected List<CharacterData.Character> listCharacterUnlocked;
    [SerializeField] protected TextMeshProUGUI txtNameChar;
    [SerializeField] protected Image imgChar;
    [SerializeField] protected List<GameObject> countSkills;

    protected virtual void Awake()
    {
        // Các phần tử UI và sự kiện chung
    }

    public override void Init()
    {
        base.Init();
        LoadData();
        InitData(0);
    }

    protected virtual void InitData(int index)
    {
        listCharacterUnlocked.Clear();
        for (int i = 0; i < characterData.listCharacter.Count; i++)
        {
            if (characterData.listCharacter[i].unlocked)
            {
                listCharacterUnlocked.Add(characterData.listCharacter[i]);
            }
        }

        ShowDefaultData(index);
    }

    protected virtual void ShowDefaultData(int indexer)
    {
        for (int i = 0; i < countSkills.Count; i++)
        {
            countSkills[i].SetActive(false);
        }
        txtNameChar.text = listCharacterUnlocked[indexer].name;
        imgChar.sprite = listCharacterUnlocked[indexer].sprite;
        Debug.Log(listCharacterUnlocked[indexer].levelSkill + " ___ at " + indexer);
        for (int i = 0; i < listCharacterUnlocked[indexer].levelSkill; i++)
        {
            countSkills[i].SetActive(true);
        }
    }

    protected virtual void ShowNextChar()
    {
        index++;
        if (index > listCharacterUnlocked.Count - 1) index = 0;
        UpdateCharacterDisplay(index);
    }

    protected virtual void ShowPreChar()
    {
        index--;
        if (index < 0) index = listCharacterUnlocked.Count - 1;
        UpdateCharacterDisplay(index);
    }

    protected void UpdateCharacterDisplay(int index)
    {
        for (int i = 0; i < countSkills.Count; i++)
        {
            countSkills[i].SetActive(false);
        }
        txtNameChar.text = listCharacterUnlocked[index].name;
        imgChar.sprite = listCharacterUnlocked[index].sprite;
        for (int i = 0; i < listCharacterUnlocked[index].levelSkill; i++)
        {
            countSkills[i].SetActive(true);
        }
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString(GameConfig.CHARACTERDATA, "");
        Debug.Log("loadData");

        if (!string.IsNullOrEmpty(json))
        {
            CharacterDataAll allQuest = JsonUtility.FromJson<CharacterDataAll>(json);
            characterData.listCharacter = allQuest.characters.ToList();
        }
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
