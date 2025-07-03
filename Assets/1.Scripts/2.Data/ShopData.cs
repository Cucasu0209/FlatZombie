using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopData", menuName = "Data/ShopData", order = 0)]

public class ShopData : ScriptableObject
{
    public List<WeaponData> Weapons;
    public List<SkinData> Skins;
}

public enum ItemTag
{
    Default,// Free when start game
    BuyInShop,// Must buy to have
    Attendance,// Attendance to have
    WatchAds,// Watch Ads reward ro collect
}