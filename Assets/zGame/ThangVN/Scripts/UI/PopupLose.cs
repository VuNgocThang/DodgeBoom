using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using BaseGame;
using TMPro;
using UnityEngine.SceneManagement;

public class PopupLose : Popup
{
    [SerializeField] EasyButton btnHome, btnPlayAgain;
    [SerializeField] TextMeshProUGUI txtTimeBest, txtTimeCurrent;

    public static async void Show()
    {
        PopupLose pop = await ManagerPopup.ShowPopup<PopupLose>();
        pop.Init();
    }

    private void Awake()
    {
        btnHome.OnClick(() =>
        {
            StartCoroutine(LoadSceneHome());
        });

        btnPlayAgain.OnClick(() =>
        {
            StartCoroutine(LoadSceneGame());
        });
    }

    public override void Init()
    {
        base.Init();
        if (LogicGame.Instance.timerCount > SaveGame.BestScore)
        {
            SaveGame.BestScore = LogicGame.Instance.timerCount;
        }

        txtTimeBest.text = string.Format("{0:00}:{1:00}", (int)SaveGame.BestScore / 60, (int)SaveGame.BestScore % 60);

        int minutes = ((int)LogicGame.Instance.timerCount / 60);
        int seconds = ((int)LogicGame.Instance.timerCount % 60);
        txtTimeCurrent.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    //public override void Hide()
    //{
    //    base.Hide();
    //    PopupHomeUI.Ins.Show();
    //}

    IEnumerator LoadSceneHome()
    {
        base.Hide();
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        ManagerEvent.ClearEvent();
        SceneManager.LoadScene("SceneHome");
    }

    IEnumerator LoadSceneGame()
    {
        base.Hide();
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        ManagerEvent.ClearEvent();
        SceneManager.LoadScene("SceneGame");
    }



}
