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
        UnlockDefaultSkin();

        LoadWeapon();
        LoadWeaponsOwned();
        UnlockDefaultWeapon();

    }
    private void Start()
    {
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
    public void CheatMoney(int target)
    {
        if (CheatManager.Instance.IsAuthenticationPass())
        {
            CurrentMoney = target;
            SaveMoney();
            OnMoneyChange?.Invoke();
        }
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
        CurrentSkinIdUsed = PlayerPrefs.GetInt(GameConfig.CURRENT_SKIN_KEY, InventoryManager.Instance.GetDefaultSkins()[0]);
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
        string data = PlayerPrefs.GetString(GameConfig.SKIN_OWNED_KEY, "");
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
    public int GetNextSkinUnlocked()
    {
        bool passByCrtP = false;
        for (int i = 0; i < SkinsOwned.Count; i++)
        {
            if (passByCrtP) return SkinsOwned[i];
            if (SkinsOwned[i] == CurrentSkinIdUsed)
            {
                passByCrtP = true;
            }
        }
        return SkinsOwned[0];
    }
    public int GetPreviousSkinUnlocked()
    {
        bool passByCrtP = false;
        for (int i = SkinsOwned.Count - 1; i >= 0; i--)
        {
            if (passByCrtP) return SkinsOwned[i];
            if (SkinsOwned[i] == CurrentSkinIdUsed)
            {
                passByCrtP = true;
            }
        }
        return SkinsOwned[SkinsOwned.Count - 1];
    }
    #endregion

    #region Weapon
    public List<int> CurrentWeaponsIdUsed { get; private set; }
    public List<int> WeaponsOwned { get; private set; }
    public Action OnCurrentWeaponUsedChange;
    public Action OnUnlockNewWeapon;
    private void SaveWeapon()
    {
        for (int i = 0; i < GameConfig.WEAPON_LIMIT; i++)
            PlayerPrefs.SetInt(GameConfig.CURRENT_SKIN_KEY + i, CurrentWeaponsIdUsed[i]);
    }
    private void LoadWeapon()
    {
        if (CurrentWeaponsIdUsed is null) CurrentWeaponsIdUsed = new List<int>();
        else CurrentWeaponsIdUsed.Clear();
        List<int> defaultIds = InventoryManager.Instance.GetDefaultWeapons();

        for (int i = 0; i < GameConfig.WEAPON_LIMIT; i++)
            CurrentWeaponsIdUsed.Add(PlayerPrefs.GetInt(GameConfig.CURRENT_SKIN_KEY + i, (i <= defaultIds.Count - 1) ? defaultIds[i] : -1));
    }
    private void SaveWeaponsOwned()
    {
        string data = "";
        for (int i = 0; i < WeaponsOwned.Count; i++)
        {
            data += WeaponsOwned[i].ToString() + ",";
        }
        PlayerPrefs.SetString(GameConfig.WEAPON_OWNED_KEY, data);
    }
    private void LoadWeaponsOwned()
    {
        if (WeaponsOwned is null) WeaponsOwned = new List<int>();
        else WeaponsOwned.Clear();


        string data = PlayerPrefs.GetString(GameConfig.WEAPON_OWNED_KEY, "");
        string[] ids = data.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            if (int.TryParse(ids[i], out int result))
            {
                WeaponsOwned.Add(result);
            }
        }

    }
    public bool HaveWeapon(int weaponIndex)
    {
        return WeaponsOwned.Contains(weaponIndex);
    }
    public void UnlockWeapon(int weaponIndex)
    {
        if (WeaponsOwned.Contains(weaponIndex) == false)
        {
            WeaponsOwned.Add(weaponIndex);
            SaveWeaponsOwned();
            OnUnlockNewWeapon?.Invoke();
        }
    }
    private void UnlockDefaultWeapon()
    {
        List<int> ids = InventoryManager.Instance.GetDefaultWeapons();
        for (int i = 0; i < ids.Count; i++)
        {
            UnlockWeapon(ids[i]);
        }
    }
    public void ChangeWeapon(int weaponId, int slotIndex)
    {
        if (WeaponsOwned.Contains(weaponId))
        {

            bool isSwap = false;
            for (int i = 0; i < CurrentWeaponsIdUsed.Count; i++)
            {
                if (CurrentWeaponsIdUsed[i] == weaponId)
                {
                    isSwap = true;
                    CurrentWeaponsIdUsed[i] = CurrentWeaponsIdUsed[slotIndex];
                    CurrentWeaponsIdUsed[slotIndex] = weaponId;
                }
            }
            if (isSwap == false)
                CurrentWeaponsIdUsed[slotIndex] = weaponId;

            SaveWeapon();
            OnCurrentWeaponUsedChange?.Invoke();
        }
    }
    public bool IsUsingWeapon(int weaponId)
    {
        return CurrentWeaponsIdUsed.Contains(weaponId);
    }
    public void EliminateWeapon(int weaponId)
    {
        for (int i = 0; i < CurrentWeaponsIdUsed.Count; i++)
        {
            if (CurrentWeaponsIdUsed[i] == weaponId)
            {
                CurrentWeaponsIdUsed[i] = -1;
                OnCurrentWeaponUsedChange?.Invoke();
                return;

            }
        }
    }
    #endregion
}
