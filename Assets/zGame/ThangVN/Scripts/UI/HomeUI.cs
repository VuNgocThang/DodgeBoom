using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using UnityEngine.SceneManagement;
using BaseGame;

public class HomeUI : MonoBehaviour
{
    public EasyButton btnSetting, btnPlay;

    private void Awake()
    {
        btnSetting.OnClick(() => PopupSettingHome.Show());

        btnPlay.OnClick(() =>
        {
            ManagerEvent.ClearEvent();
            SceneManager.LoadScene("SceneGame");
        });
    }

    private void Start()
    {
        if (SaveGame.Music) ManagerAudio.PlayMusic(ManagerAudio.Data.musicBG);
        else ManagerAudio.PauseMusic();
    }
}
