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


    public List<SkinData> SkinDatas { get; private set; }
    public int CurrentSkinIdSelected { get; private set; }
    public Action OnSelectSkin;

    private void UpdateSkinList()
    {
        if (ShopData is null) LoadShopData();
        if (ShopData != null)
        {
            if (SkinDatas is null) SkinDatas = new List<SkinData>();
            else WeaponDatas.Clear();

            for (int i = 0; i < ShopData.Skins.Count; i++)
            {
                SkinDatas.Add(ShopData.Skins[i]);
            }
        }
    }
    public void SelectSkin(int skinId)
    {
        if (skinId != CurrentSkinIdSelected)
        {
            CurrentSkinIdSelected = skinId;
            OnSelectSkin?.Invoke();
        }
    }
    public void RequestBuySkin(int skinId)
    {

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
    #endregion

    #region Weapon Data
    public Dictionary<WeaponType, List<WeaponData>> WeaponDatas { get; private set; }
    private void UpdateWeaponList()
    {
        if (ShopData is null) LoadShopData();
        if (ShopData != null)
        {
            if (WeaponDatas is null) WeaponDatas = new Dictionary<WeaponType, List<WeaponData>>();
            else WeaponDatas.Clear();

            for (int i = 0; i < ShopData.Weapons.Count; i++)
            {
                if (WeaponDatas.ContainsKey(ShopData.Weapons[i].Type))
                {
                    WeaponDatas[ShopData.Weapons[i].Type].Add(ShopData.Weapons[i]);
                }
                else
                {
                    WeaponDatas.Add(ShopData.Weapons[i].Type, new List<WeaponData>() { ShopData.Weapons[i] });
                }
            }
        }

    }
    #endregion
}