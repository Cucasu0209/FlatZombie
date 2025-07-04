using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Simple Singleton
    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;

        LoadSkinData();
        LoadWeaponData();
        UpdateWeaponList();
        UpdateSkinList();
    }
    #endregion

    #region Unity
    private void Start()
    {

    }
    private void Update()
    {

    }
    private void OnDestroy()
    {

    }
    #endregion

    #region Skin Data
    public SkinData SkinData { get; private set; }

    public Dictionary<int, SkinDetailData> SkinDataDict { get; private set; }
    public int CurrentSkinIdSelected { get; private set; }
    public Action OnSelectSkin;
    public void DownloadSkinData(string newSID, string newGID, Action<List<List<string>>> OnSuccess, Action OnFail)
    {
        SkinData.LoadDataFromServer(newSID, newGID, OnSuccess, OnFail);
    }
    private void LoadSkinData()
    {
        if (SkinData is null)
        {
            SkinData = Resources.Load<SkinData>(GameConfig.SKIN_DATA_LINK);
        }
    }
    private void UpdateSkinList()
    {
        if (SkinData is null) LoadSkinData();
        if (SkinData != null)
        {
            if (SkinDataDict is null) SkinDataDict = new Dictionary<int, SkinDetailData>();
            else SkinDataDict.Clear();

            for (int i = 0; i < SkinData.Skins.Count; i++)
            {
                SkinDataDict.Add(SkinData.Skins[i].ID, SkinData.Skins[i]);
            }
        }
    }
    public void SelectSkin(int skinId)
    {
        if (skinId != CurrentSkinIdSelected)
        {
            CurrentSkinIdSelected = skinId;
            OnSelectSkin?.Invoke();
            if (PlayerData.Instance.HaveSkin(CurrentSkinIdSelected))
            {
                PlayerData.Instance.ChangeSkin(CurrentSkinIdSelected);
            }
        }
    }
    public void RequestBuySkin(int skinId)
    {
        SkinDetailData skin = GetSkinDataById(skinId);
        if (skin != null)
        {
            PlayerData.Instance.BuyItem(skin.Price,
            () =>
            {
                SelectSkin(skinId);
                PlayerData.Instance.UnlockSkin(skinId);
                PlayerData.Instance.ChangeSkin(skinId);
            },
            () =>
            {
                Debug.LogError("NOT ENOUGH MONEY");
            });
        }
    }
    public List<int> GetDefaultSkins()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < SkinData.Skins.Count; i++)
        {
            if (SkinData.Skins[i].Tag == ItemTag.Default) result.Add(SkinData.Skins[i].ID);
        }
        return result;
    }
    public SkinDetailData GetSkinDataById(int id)
    {
        if (SkinDataDict.ContainsKey(id)) return SkinDataDict[id];
        return null;
    }
    #endregion

    #region Weapon Data
    public WeaponData WeaponData { get; private set; }
    private void LoadWeaponData()
    {
        if (WeaponData is null)
        {
            WeaponData = Resources.Load<WeaponData>(GameConfig.WEAPON_DATA_LINK);
        }
    }
    public WeaponType CurrentCategory { get; private set; } = WeaponType.Melee;
    public Action OnChangeCategory;
    public Dictionary<WeaponType, List<WeaponDetailData>> WeaponDatasDict_Type { get; private set; }
    public Dictionary<int, WeaponDetailData> WeaponDatasDict_All { get; private set; }
    public int CurrentWeaponIdSelected { get; private set; }
    public Action OnSelectWeapon;
    public void DownloadWeaponData(string newSID, string newGID, Action<List<List<string>>> OnSuccess, Action OnFail)
    {
        WeaponData.LoadDataFromServer(newSID, newGID, OnSuccess, OnFail);
    }
    private void UpdateWeaponList()
    {
        if (WeaponData is null) LoadWeaponData();
        if (WeaponData != null)
        {
            if (WeaponDatasDict_Type is null) WeaponDatasDict_Type = new Dictionary<WeaponType, List<WeaponDetailData>>();
            else WeaponDatasDict_Type.Clear();

            if (WeaponDatasDict_All is null) WeaponDatasDict_All = new Dictionary<int, WeaponDetailData>();
            else WeaponDatasDict_All.Clear();

            for (int i = 0; i < WeaponData.Weapons.Count; i++)
            {
                if (WeaponDatasDict_Type.ContainsKey(WeaponData.Weapons[i].Type))
                {
                    WeaponDatasDict_Type[WeaponData.Weapons[i].Type].Add(WeaponData.Weapons[i]);
                }
                else
                {
                    WeaponDatasDict_Type.Add(WeaponData.Weapons[i].Type, new List<WeaponDetailData>() { WeaponData.Weapons[i] });
                }

                WeaponDatasDict_All.Add(WeaponData.Weapons[i].ID, WeaponData.Weapons[i]);
            }
        }

    }
    public void SelectWeapon(int weaponId)
    {
        CurrentWeaponIdSelected = weaponId;
        OnSelectWeapon?.Invoke();
    }
    public void ChangeCategory(WeaponType category)
    {
        CurrentCategory = category;
        OnChangeCategory?.Invoke();
    }
    public void RequestBuyWeapon(int weaponId)
    {
        WeaponDetailData weapon = GetWeaponDataById(weaponId);
        if (weapon != null)
        {
            PlayerData.Instance.BuyItem(weapon.Price,
            () =>
            {
                SelectWeapon(weaponId);
                PlayerData.Instance.UnlockWeapon(weaponId);
                UIGlobalManager.Instance.OnOpenSelectWeaponSlotPopup((slotIndex) =>
                {
                    PlayerData.Instance.ChangeWeapon(weaponId, slotIndex);
                },
                () =>
                {

                });
            },
            () =>
            {
                Debug.LogError("NOT ENOUGH MONEY");
            });
        }
    }
    public WeaponDetailData GetWeaponDataById(int id)
    {
        if (WeaponDatasDict_All.ContainsKey(id)) return WeaponDatasDict_All[id];
        return null;
    }
    public List<int> GetDefaultWeapons()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < WeaponData.Weapons.Count; i++)
        {
            if (WeaponData.Weapons[i].Tag == ItemTag.Default) result.Add(WeaponData.Weapons[i].ID);
        }
        return result;
    }
    #endregion
}