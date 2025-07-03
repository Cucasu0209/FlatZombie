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

        LoadShopData();
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

    #region Shop Data
    public ShopData ShopData { get; private set; }
    private void LoadShopData()
    {
        if (ShopData is null)
        {
            ShopData = Resources.Load<ShopData>(GameConfig.SHOP_DATA_LINK);
        }
    }
    #endregion

    #region Skin Data
    public Dictionary<int, SkinData> SkinDatas { get; private set; }
    public int CurrentSkinIdSelected { get; private set; }
    public Action OnSelectSkin;
    private void UpdateSkinList()
    {
        if (ShopData is null) LoadShopData();
        if (ShopData != null)
        {
            if (SkinDatas is null) SkinDatas = new Dictionary<int, SkinData>();
            else SkinDatas.Clear();

            for (int i = 0; i < ShopData.Skins.Count; i++)
            {
                SkinDatas.Add(ShopData.Skins[i].ID, ShopData.Skins[i]);
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
        SkinData skin = GetSkinDataById(skinId);
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
        for (int i = 0; i < ShopData.Skins.Count; i++)
        {
            if (ShopData.Skins[i].Tag == ItemTag.Default) result.Add(ShopData.Skins[i].ID);
        }
        return result;
    }
    public SkinData GetSkinDataById(int id)
    {
        if (SkinDatas.ContainsKey(id)) return SkinDatas[id];
        return null;
    }
    #endregion

    #region Weapon Data
    public WeaponType CurrentCategory { get; private set; } = WeaponType.Melee;
    public Action OnChangeCategory;
    public Dictionary<WeaponType, List<WeaponData>> WeaponDatas_Type { get; private set; }
    public Dictionary<int, WeaponData> WeaponDatas_All { get; private set; }
    public int CurrentWeaponIdSelected { get; private set; }
    public Action OnSelectWeapon;
    private void UpdateWeaponList()
    {
        if (ShopData is null) LoadShopData();
        if (ShopData != null)
        {
            if (WeaponDatas_Type is null) WeaponDatas_Type = new Dictionary<WeaponType, List<WeaponData>>();
            else WeaponDatas_Type.Clear();

            if (WeaponDatas_All is null) WeaponDatas_All = new Dictionary<int, WeaponData>();
            else WeaponDatas_All.Clear();

            for (int i = 0; i < ShopData.Weapons.Count; i++)
            {
                if (WeaponDatas_Type.ContainsKey(ShopData.Weapons[i].Type))
                {
                    WeaponDatas_Type[ShopData.Weapons[i].Type].Add(ShopData.Weapons[i]);
                }
                else
                {
                    WeaponDatas_Type.Add(ShopData.Weapons[i].Type, new List<WeaponData>() { ShopData.Weapons[i] });
                }

                WeaponDatas_All.Add(ShopData.Weapons[i].ID, ShopData.Weapons[i]);
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
        WeaponData weapon = GetWeaponDataById(weaponId);
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
    public WeaponData GetWeaponDataById(int id)
    {
        if (WeaponDatas_All.ContainsKey(id)) return WeaponDatas_All[id];
        return null;
    }
    public List<int> GetDefaultWeapons()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < ShopData.Weapons.Count; i++)
        {
            if (ShopData.Weapons[i].Tag == ItemTag.Default) result.Add(ShopData.Weapons[i].ID);
        }
        return result;
    }
    #endregion
}