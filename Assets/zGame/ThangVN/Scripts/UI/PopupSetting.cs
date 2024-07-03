using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using BaseGame;

public class PopupSetting : Popup
{
    public EasyButton btnSound, btnMusic;
    public GameObject imgMusicOff, imgSoundOff;

    public static async void Show()
    {
        PopupSetting pop = await ManagerPopup.ShowPopup<PopupSetting>();
        pop.Init();
    }

    private void Awake()
    {
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

        imgMusicOff.SetActive(!SaveGame.Music);
        imgSoundOff.SetActive(!SaveGame.Sound);
    }

    public override void Hide()
    {
        base.Hide();
        PopupHomeUI.Ins.Show();
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
}
