using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;

public class PopupShopItem : Popup
{
    [SerializeField] TextMeshProUGUI txtCoin;
    public static async void Show()
    {
        PopupShopItem pop = await ManagerPopup.ShowPopup<PopupShopItem>();
        pop.Init();
    }

    public override void Init()
    {
        base.Init();
        txtCoin.text = SaveGame.Coin.ToString();
    }

    public override void Hide()
    {
        base.Hide();
        PopupShop.Show();
    }


    private void Update()
    {
        txtCoin.text = SaveGame.Coin.ToString();
    }



}
