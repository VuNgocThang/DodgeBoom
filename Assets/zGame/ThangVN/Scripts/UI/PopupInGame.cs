using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;
using UnityEngine.UI;

public class PopupInGame : MonoBehaviour
{
    [SerializeField] EasyButton btnMenu, btnUseEffect;
    [SerializeField] TextMeshProUGUI txtCoin, txtTimer, txtTimeCountDown;
    [SerializeField] GameObject countDownTimeEffect;
    [SerializeField] float timeCountDownEffect = -1;
    [SerializeField] private Image imgProgress;
    [SerializeField] Animator animBtnUseEffect;

    private void Awake()
    {
        btnMenu.OnClick(ShowMenu);

        btnUseEffect.OnClick(() =>
        {
            ManagerEvent.RaiseEvent(EventCMD.EVENT_USE_EFFECT);
            LogicGame.Instance.isUseEnergy = true;
            countDownTimeEffect.SetActive(true);
            timeCountDownEffect = 10f;
        });
    }

    void ShowMenu()
    {

    }

    private void Update()
    {
        txtCoin.text = SaveGame.Coin.ToString();
        int minutes = ((int)LogicGame.Instance.timerCount / 60);
        int seconds = ((int)LogicGame.Instance.timerCount % 60);
        txtTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        imgProgress.fillAmount = (float)SaveGame.Energy / 50;

        if (imgProgress.fillAmount >= 1) animBtnUseEffect.Play("appear");

        if (timeCountDownEffect > 0) timeCountDownEffect -= Time.deltaTime;
        else
        {
            timeCountDownEffect = 0;
            countDownTimeEffect.SetActive(false);
            LogicGame.Instance.isUseEnergy = false;
        }
        txtTimeCountDown.text = Mathf.RoundToInt(timeCountDownEffect).ToString();
    }
}
