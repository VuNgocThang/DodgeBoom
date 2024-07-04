using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;
using UnityEngine.UI;

public class PopupInGame : MonoBehaviour
{
    public static PopupInGame Instance;
    [SerializeField] EasyButton btnMenu, btnUseEffect;
    [SerializeField] TextMeshProUGUI txtCoin, txtTimer, txtTimeCountDown;
    [SerializeField] GameObject countDownTimeEffect;
    [SerializeField] float timeCountDownEffect = -1;
    [SerializeField] private Image imgProgress;
    [SerializeField] Animator animBtnUseEffect;
    [SerializeField] List<GameObject> listHeart;
    public bool canPlay;

    public Transform coinUIPosition;
    public Transform energyUIPosition;

    private void Awake()
    {
        Instance = this;
        btnMenu.OnClick(ShowMenu);

        btnUseEffect.OnClick(() =>
        {
            ManagerEvent.RaiseEvent(EventCMD.EVENT_USE_EFFECT);
            LogicGame.Instance.isUseEnergy = true;
            countDownTimeEffect.SetActive(true);
            timeCountDownEffect = 10f;
            canPlay = true;
        });

        ManagerEvent.RegEvent(EventCMD.EVENT_LOSE, ShowPopupLose);
        ManagerEvent.RegEvent(EventCMD.EVENT_LOSE_HEART, DecreaseHeart);
    }

    void ShowMenu()
    {
        PopupSettingGame.Show();
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
            if (canPlay) StartCoroutine(PlayAnimHide());
        }
        txtTimeCountDown.text = Mathf.RoundToInt(timeCountDownEffect).ToString();
    }


    IEnumerator PlayAnimHide()
    {
        animBtnUseEffect.Play("hide");
        yield return new WaitForSeconds(0.25f);
        canPlay = false;
    }

    void ShowPopupLose(object e)
    {
        PopupLose.Show();
    }

    void DecreaseHeart(object e)
    {
        listHeart.RemoveAt(listHeart.Count - 1);
    }
}
