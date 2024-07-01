using ntDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHome : MonoBehaviour
{
    [SerializeField] private Image imgProgress;
    [SerializeField] EasyButton btnUseEffect;
    [SerializeField] Animator animBtnUseEffect;
    private void Awake()
    {
        btnUseEffect.OnClick(() => ManagerEvent.RaiseEvent(EventCMD.EVENT_USE_EFFECT));
    }

    private void Update()
    {
        imgProgress.fillAmount = (float)SaveGame.Energy / 50;

        if (imgProgress.fillAmount >= 1)
        {
            animBtnUseEffect.Play("appear");
        }
    }

}
