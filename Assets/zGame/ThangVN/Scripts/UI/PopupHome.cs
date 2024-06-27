using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using BaseGame;

public class PopupHome : MonoBehaviour
{
    public EasyButton btnSetting, btnSwitch;
    public TextMeshProUGUI txtPoint, txtLevel;
    public Image imgFill;
    [SerializeField] Animator animBtnSwitch;
    bool canClick = true;

    private void Awake()
    {
        btnSetting.OnClick(() => PopupSetting.Show());
        btnSwitch.OnClick(() =>
        {
            if (canClick)
            {
                ManagerEvent.RaiseEvent(EventCMD.EVENT_SWITCH);
                ManagerAudio.PlaySound(ManagerAudio.Data.soundSwitch);
                StartCoroutine(OnClickSwitch());
            }
        });
        ManagerEvent.RegEvent(EventCMD.EVENT_POINT, UpdatePoint);
        ManagerEvent.RegEvent(EventCMD.EVENT_WIN, ShowPopupWin);
        ManagerEvent.RegEvent(EventCMD.EVENT_LOSE, ShowPopupLose);
    }

    void Start()
    {
        if (SaveGame.Music) ManagerAudio.PlayMusic(ManagerAudio.Data.musicInGame);
        else ManagerAudio.PauseMusic();

        txtLevel.text = $"Level {SaveGame.Level + 1}";

        //imgFill.fillAmount = (float)LogicGame.point / (float)LogicGame.maxPoint;
        //txtPoint.text = $"{LogicGame.point}/{LogicGame.maxPoint}";
    }

    void UpdatePoint(object e)
    {
        //LogicGame.point += (int)e;

        //if (LogicGame.point >= LogicGame.maxPoint) LogicGame.point = LogicGame.maxPoint;

        //imgFill.fillAmount = (float)LogicGame.point / (float)LogicGame.maxPoint;
        //txtPoint.text = $"{LogicGame.point}/{LogicGame.maxPoint}";
    }

    void ShowPopupWin(object e)
    {
        PopupWin.Show();
    }

    void ShowPopupLose(object e)
    {
        PopupLose.Show();
    }

    IEnumerator OnClickSwitch()
    {
        canClick = false;
        if (animBtnSwitch != null) animBtnSwitch.Play("Click");
        yield return new WaitForSeconds(0.2f);
        canClick = true;
    }
}
