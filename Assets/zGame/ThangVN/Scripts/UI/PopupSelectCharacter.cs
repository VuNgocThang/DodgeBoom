using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using UnityEngine.SceneManagement;

public class PopupSelectCharacter : PopupCharacterBase
{
    public EasyButton btnNext, btnPrevious, btnPlay;
    public static async void Show()
    {
        PopupSelectCharacter pop = await ManagerPopup.ShowPopup<PopupSelectCharacter>();
        pop.Init();
    }

    protected override void Awake()
    {
        base.Awake();

        btnNext.OnClick(ShowNextChar);
        btnPrevious.OnClick(ShowPreChar);
        btnPlay.OnClick(PlayGame);
    }
    public override void Hide()
    {
        base.Hide();
        PopupHomeUI.Ins.Show();
    }

    void PlayGame()
    {
        base.Hide();
        characterData.indexToPlay = index;

        CharacterData.Character charData = characterData.listCharacter.Find(x => x.index == characterData.indexToPlay);
        if (charData != null)
        {
            characterData.nameToPlay = charData.name;
        }

        SaveData();
        Debug.Log(characterData.nameToPlay);

        StartCoroutine(ShowUIGame());
    }

    IEnumerator ShowUIGame()
    {
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        SceneManager.LoadScene("SceneGame");
        // Logic UI Game Show here;
    }
}
