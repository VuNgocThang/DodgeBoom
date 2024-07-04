using ntDev;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public class CharacterDataAll
{
    public CharacterData.Character[] characters;
}

public class PopupShopCharacter : PopupCharacterBase
{
    [SerializeField] EasyButton btnNext, btnPrevious, btnUpgrade, btnBuyCharacter;
    [SerializeField] TextMeshProUGUI txtCoin;
    public static async void Show()
    {
        PopupShopCharacter pop = await ManagerPopup.ShowPopup<PopupShopCharacter>();
        pop.Init();
        pop.InitShop();
    }

    void InitShop()
    {
        txtCoin.text = SaveGame.Coin.ToString();
    }

    protected override void Awake()
    {
        base.Awake();

        btnNext.OnClick(ShowNextChar);
        btnPrevious.OnClick(ShowPreChar);
        btnUpgrade.OnClick(Upgrade);
        btnBuyCharacter.OnClick(BuyNewCharacter);
    }
    public override void Hide()
    {
        base.Hide();
        StartCoroutine(ShowPopupShop());
    }
    IEnumerator ShowPopupShop()
    {
        yield return new WaitForSeconds(GameConfig.TIMEHIDE);
        PopupShop.Show();
    }

    void Upgrade()
    {
        CharacterData.Character charData = characterData.listCharacter.Find(x => x.index == index);
        if (charData != null)
        {
            if (charData.levelSkill >= 3) return;
            else charData.levelSkill++;
        }
        SaveData();
        InitData(index);
    }

    void BuyNewCharacter()
    {
        if (characterData.indexLastChar >= characterData.listCharacter.Count - 1) return;
        characterData.indexLastChar++;

        CharacterData.Character charData = characterData.listCharacter.Find(x => x.index == characterData.indexLastChar);
        if (charData != null)
        {
            charData.unlocked = true;
        }

        SaveData();
        index = characterData.indexLastChar;
        InitData(index);
    }
}