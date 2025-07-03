using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("General Information")]
    public int ID;
    public string Name;
    public WeaponType Type;
    public ItemTag Tag;

    [Header("Detail")]
    public float Damage;
    public float HeadShotDamage;
    public float FireRate;
    public int Magazine;
    public int Price;

}

public enum WeaponType
{
    Melee,
    One_Hand_Gun,
    Two_Hand_Gun,
}