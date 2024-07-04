using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using BaseGame;

public class PopupSettingInGame : Popup
{
    public EasyButton btnContinue, btnSound, btnMusic, btnExit;
    public GameObject imgMusicOff, imgSoundOff;

    public static async void Show()
    {
        PopupSettingInGame pop = await ManagerPopup.ShowPopup<PopupSettingInGame>();
        pop.Init();
    }

    private void Awake()
    {
        btnContinue.OnClick(ContinueGame);

        btnMusic.OnClick(() =>
        {
            ToggleBtnMusic();
        });
        btnSound.OnClick(() =>
        {
            ToggleBtnSound();
        });
    }

    public override void Init()
    {
        base.Init();
        LogicGame.Instance.isPauseGame = true;
        imgMusicOff.SetActive(!SaveGame.Music);
        imgSoundOff.SetActive(!SaveGame.Sound);
    }

    void ToggleBtnMusic()
    {
        if (SaveGame.Music) ManagerAudio.MuteMusic();
        else
        {
            //ManagerAudio.PlayMusic(ManagerAudio.Data.musicInGame);
            ManagerAudio.UnMuteMusic();
        }

        SaveGame.Music = !SaveGame.Music;
        imgMusicOff.SetActive(!SaveGame.Music);
    }

    void ToggleBtnSound()
    {
        if (SaveGame.Sound) ManagerAudio.MuteSound();
        else ManagerAudio.UnMuteSound();

        SaveGame.Sound = !SaveGame.Sound;
        imgSoundOff.SetActive(!SaveGame.Sound);
    }

    void ContinueGame()
    {
        Debug.Log("Continue Game");
        LogicGame.Instance.isPauseGame = false;

        //if (animator != null) animator.Play("Hide");
    }
}
