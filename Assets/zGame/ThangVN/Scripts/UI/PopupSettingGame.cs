using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using BaseGame;
using UnityEngine.SceneManagement;

public class PopupSettingGame : Popup
{
    public EasyButton btnContinue, btnSound, btnMusic, btnExit;
    public GameObject imgMusicOff, imgSoundOff;

    public static async void Show()
    {
        PopupSettingGame pop = await ManagerPopup.ShowPopup<PopupSettingGame>();
        pop.Init();
    }

    private void Awake()
    {
        btnContinue.OnClick(() => StartCoroutine(ContinueGame()));

        btnMusic.OnClick(() =>
        {
            ToggleBtnMusic();
        });
        btnSound.OnClick(() =>
        {
            ToggleBtnSound();
        });

        btnExit.OnClick(() => SceneManager.LoadScene("SceneHome"));
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

    IEnumerator ContinueGame()
    {
        if (animator != null) animator.Play("Hide");
        yield return new WaitForSeconds(0.13f);
        Debug.Log("Continue Game");
        LogicGame.Instance.isPauseGame = false;
        gameObject.SetActive(false);
        ManagerEvent.RaiseEvent(EventCMD.EVENT_POPUP_CLOSE, this);
    }
}
