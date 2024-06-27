using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using UnityEngine.SceneManagement;
using DG.Tweening;
using BaseGame;

public class PopupWin : Popup
{
    public EasyButton btnContinue;
    public GameObject vfx;
    public static async void Show()
    {
        PopupWin pop = await ManagerPopup.ShowPopup<PopupWin>();
        pop.Init();
    }

    private void Awake()
    {
        btnContinue.OnClick(() =>
        {
            ManagerEvent.ClearEvent();
            ManagerPopup.HidePopup<PopupWin>();
            SceneManager.LoadScene("SceneGame");
        });
    }

    public override void Init()
    {
        base.Init();
        ManagerAudio.PlaySound(ManagerAudio.Data.soundPaperFireWorks);
        ManagerAudio.PlaySound(ManagerAudio.Data.soundPopupWin);
        Debug.Log("init popup win");
    }

    private void Update()
    {
        vfx.transform.Rotate(new Vector3(0, 0, 1) * -20f * Time.deltaTime);
    }
}
