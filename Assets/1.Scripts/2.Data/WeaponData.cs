using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    public int ID;
    public string Name;
    public int Price;
    public WeaponType Type;
    public ItemTag Tag;
}

public enum WeaponType
{
    Melee,
    One_Hand_Gun,
    Two_Hand_Gun,
}