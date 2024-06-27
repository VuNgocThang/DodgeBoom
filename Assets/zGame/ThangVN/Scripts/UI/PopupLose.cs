using BaseGame;
using ntDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupLose : Popup
{
    public EasyButton btnRetry;
    public static async void Show()
    {
        PopupLose pop = await ManagerPopup.ShowPopup<PopupLose>();
        pop.Init();
    }

    private void Awake()
    {
        btnRetry.OnClick(() =>
        {
            ManagerEvent.ClearEvent();
            //ManagerPopup.HidePopup<PopupLose>();
            Hide();
        });
    }

    public override void Init()
    {
        base.Init();
        ManagerAudio.PlaySound(ManagerAudio.Data.soundPopupLose);
        Debug.Log("init popup lose");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Hide popup lose");
        SceneManager.LoadScene("SceneGame");
    }
}
