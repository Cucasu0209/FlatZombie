using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkinData", menuName = "Data/SkinData", order = 0)]

public class SkinData : ScriptableObject
{
    public int ID;
    public string Name;
    public int HP;
    public int Price;
    public ItemTag Tag;
}

