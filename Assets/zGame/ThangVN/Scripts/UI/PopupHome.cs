using ntDev;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupHome : MonoBehaviour
{
    [SerializeField] private Image imgProgress;
    [SerializeField] EasyButton btnUseEffect;
    [SerializeField] Animator animBtnUseEffect;
    [SerializeField] GameObject countDownTimeEffect;
    [SerializeField] TextMeshProUGUI txtTimeCountDown;
    [SerializeField] float timeCountDownEffect = -1;
    private void Awake()
    {
        btnUseEffect.OnClick(() =>
        {
            ManagerEvent.RaiseEvent(EventCMD.EVENT_USE_EFFECT);
            LogicGame.Instance.isUseEnergy = true;
            countDownTimeEffect.SetActive(true);
            timeCountDownEffect = 10f;
        });
    }

    private void Update()
    {
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
