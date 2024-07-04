using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntDev;
using TMPro;

public class PopupShopCoin : Popup
{
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] EasyButton btn500, btn1000, btn1500, btn2000;
    private void Awake()
    {
        btn500.OnClick(() => SaveGame.Coin += 500);
        btn1000.OnClick(() => SaveGame.Coin += 1000);
        btn1500.OnClick(() => SaveGame.Coin += 1500);
        btn2000.OnClick(() => SaveGame.Coin += 2000);
    }
    public static async void Show()
    {
        PopupShopCoin pop = await ManagerPopup.ShowPopup<PopupShopCoin>();
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
        StartCoroutine(ShowPopupShop());
    }

    private void Update()
    {
        txtCoin.text = SaveGame.Coin.ToString();
    }
    IEnumerator ShowPopupShop()
    {
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupShop.Show();
    }
}
