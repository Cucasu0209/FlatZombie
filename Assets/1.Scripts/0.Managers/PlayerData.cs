using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private void Awake()
    {
        Instance = this;
        LoadMoney();
        LoadSkin();
        LoadSkinsOwned();
    }
    private void Start()
    {
        UnlockDefaultSkin();
    }

    #region Money
    public int CurrentMoney { get; private set; }
    public Action OnMoneyChange;

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        SaveMoney();
        OnMoneyChange?.Invoke();
    }
    public bool EnoughMoney(int price)
    {
        return CurrentMoney >= price;
    }
    public void BuyItem(int price, Action BuyComplete = null, Action BuyFail = null)
    {
        if (EnoughMoney(price))
        {
            CurrentMoney -= price;
            SaveMoney();
            OnMoneyChange?.Invoke();
            BuyComplete?.Invoke();
        }
        else
        {
            BuyFail?.Invoke();
        }
    }
    private void SaveMoney()
    {
        PlayerPrefs.GetInt(GameConfig.MONEY_AMOUNT_KEY, CurrentMoney);
    }
    private void LoadMoney()
    {
        CurrentMoney = PlayerPrefs.GetInt(GameConfig.MONEY_AMOUNT_KEY, 0);
    }
    #endregion

    #region Skin
    public int CurrentSkinIdUsed { get; private set; }
    public List<int> SkinsOwned { get; private set; }
    public Action OnCurrentSkinUsedChange;
    public Action<int> OnUnlockNewSkin;
    private void SaveSkin()
    {
        PlayerPrefs.SetInt(GameConfig.CURRENT_SKIN_KEY, CurrentSkinIdUsed);
    }
    private void LoadSkin()
    {
        CurrentSkinIdUsed = PlayerPrefs.GetInt(GameConfig.CURRENT_SKIN_KEY, 0);
    }
    private void SaveSkinsOwned()
    {
        string data = "";
        for (int i = 0; i < SkinsOwned.Count; i++)
        {
            data += SkinsOwned[i].ToString() + ",";
        }
        PlayerPrefs.SetString(GameConfig.SKIN_OWNED_KEY, data);
    }
    private void LoadSkinsOwned()
    {
        if (SkinsOwned is null) SkinsOwned = new List<int>();
        else SkinsOwned.Clear();
        string data = PlayerPrefs.GetString(GameConfig.SKIN_OWNED_KEY, "0,");
        string[] ids = data.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            if (int.TryParse(ids[i], out int result))
            {
                SkinsOwned.Add(result);
            }
        }

    }
    private void UnlockDefaultSkin()
    {
        List<int> ids = InventoryManager.Instance.GetDefaultSkins();
        for (int i = 0; i < ids.Count; i++)
        {
            UnlockSkin(ids[i]);
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        if (SkinsOwned.Contains(skinIndex) == false)
        {
            SkinsOwned.Add(skinIndex);
            SaveSkinsOwned();
            OnUnlockNewSkin?.Invoke(skinIndex);
        }
    }
    public void ChangeSkin(int skinIndex)
    {
        if (SkinsOwned.Contains(skinIndex))
        {
            CurrentSkinIdUsed = skinIndex;
            SaveSkin();
            OnCurrentSkinUsedChange?.Invoke();
        }
    }
    public bool HaveSkin(int skinIndex)
    {
        return SkinsOwned.Contains(skinIndex);
    }
    #endregion


}
