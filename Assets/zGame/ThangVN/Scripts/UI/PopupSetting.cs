using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using UnityEngine.SceneManagement;
using BaseGame;

public class PopupSetting : Popup
{
    public EasyButton btnSound, btnMusic, btnRestart, btnHome;
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

        btnRestart.OnClick(() =>
        {
            ManagerEvent.ClearEvent();
            StartCoroutine(LoadScene("SceneGame"));
        });

        btnHome.OnClick(() =>
        {
            ManagerEvent.ClearEvent();
            StartCoroutine(LoadScene("SceneHome"));
        });
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("init setting in game");
        imgMusicOff.SetActive(!SaveGame.Music);
        imgSoundOff.SetActive(!SaveGame.Sound);
    }

    public override void Hide()
    {
        base.Hide();
    }

    void ToggleBtnMusic()
    {
        if (SaveGame.Music) ManagerAudio.MuteMusic();
        else
        {
            ManagerAudio.PlayMusic(ManagerAudio.Data.musicInGame);
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

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}
