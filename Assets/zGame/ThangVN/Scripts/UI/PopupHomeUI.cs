using System.Collections;
using System.Collections.Generic;
using ntDev;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine;

public class PopupHomeUI : MonoBehaviour
{
    public static PopupHomeUI Ins;
    [SerializeField] EasyButton btnPlay, btnShop, btnNoAds, btnSetting;
    [SerializeField] public Animator anim;

    private void Awake()
    {
        Ins = this;

        btnSetting.OnClick(() => StartCoroutine(ShowPopupSetting()));

        btnShop.OnClick(() => StartCoroutine(ShowPopupShop()));

        btnPlay.OnClick(() => StartCoroutine(LoadSceneGame()));
    }
    public void Show()
    {
        StartCoroutine(PlayAnimShow());
    }

    IEnumerator ShowPopupSetting()
    {
        anim.Play("Hide");
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupSetting.Show();
    }

    IEnumerator ShowPopupShop()
    {
        anim.Play("Hide");
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupShop.Show();
    }

    IEnumerator LoadSceneGame()
    {
        anim.Play("Hide");
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupSelectCharacter.Show();
    }

    IEnumerator PlayAnimShow()
    {
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        anim.Play("Show");
    }
}
