using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{

    //PlayerPrefs Key
    public const string MONEY_AMOUNT_KEY = "MONEY_AMOUNT";
    public const string CURRENT_SKIN_KEY = "CURRENT_SKIN";
    public const string CURRENT_WEAPON_KEY = "CURRENT_WEAPON";
    public const string SKIN_OWNED_KEY = "SKIN_OWNED";
    public const string WEAPON_OWNED_KEY = "WEAPON_OWNED";
    public const string CHEAT_PASSWORD_KEY = "CHEAT_PASSWORD";


    //Resources Link
    public const string SKIN_DATA_LINK = "Data/SkinData";
    public const string WEAPON_DATA_LINK = "Data/WeaponData";
    public const string ZOMBIE_DATA_LINK = "Data/ZombieData";
    public const string LEVEL_DATA_LINK = "Data/LevelData";

    //Cheat
    public const string CHEAT_CORRECT_PASSWORD = "apusapus";

    //Weapon config
    public const int WEAPON_LIMIT = 2;
    public const float WEAPON_DAMAGE_MAX = 100;
    public const float WEAPON_HEADSHOT_DAMAGE_MAX = 150;
    public const float WEAPON_FIRERATE_MAX = 1000;
    public const int WEAPON_MAGAZINE_MAX = 50;
}
