using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
public class PopupShop : Popup
{
    public EasyButton btnCharacter, btnItem, btnCoin;
    public static async void Show()
    {
        PopupShop pop = await ManagerPopup.ShowPopup<PopupShop>();
        pop.Init();
    }

    private void Awake()
    {
        btnCharacter.OnClick(() => StartCoroutine(ShowPopupCharacters()));

        btnItem.OnClick(() => StartCoroutine(ShowPopupItem()));

        btnCoin.OnClick(() => StartCoroutine(ShowPopupCoin()));
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Hide()
    {
        base.Hide();
        PopupHomeUI.Ins.Show();
    }


    IEnumerator ShowPopupCharacters()
    {
        base.Hide();
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupShopCharacter.Show();
    }

    IEnumerator ShowPopupItem()
    {
        base.Hide();
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        //Show Popup Item;

    }

    IEnumerator ShowPopupCoin()
    {
        base.Hide();
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        //Show Popup Coin;

    }
}
