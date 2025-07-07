using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;


#if UNITY_EDITOR

using UnityEditor;
#endif
[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    public string S_ID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    public string G_ID = "118954083";
    public List<WeaponDetailData> Weapons;

    public async void LoadDataFromServer(string newSID, string newGID, Action<List<List<string>>> OnSuccess = null, Action OnFail = null)
    {
        S_ID = newSID;
        G_ID = newGID;
        await CloudCSVLoader.LoadSheetAsync(S_ID, G_ID,
            (data) =>
            {
                Weapons = new List<WeaponDetailData>();
                for (int i = 1; i < data.Count; i++)
                {
                    WeaponDetailData wp = new WeaponDetailData();

                    for (int j = 0; j < data[i].Count; j++)
                    {
                        switch (data[0][j].Trim())
                        {
                            case nameof(wp.ID): int.TryParse(data[i][j], out wp.ID); break;
                            case nameof(wp.Name): wp.Name = data[i][j]; break;
                            case nameof(wp.Damage): int.TryParse(data[i][j], out wp.Damage); break;
                            case nameof(wp.HeadshotDamage): int.TryParse(data[i][j], out wp.HeadshotDamage); break;
                            case nameof(wp.FireRate): int.TryParse(data[i][j], out wp.FireRate); break;
                            case nameof(wp.Magazine): int.TryParse(data[i][j], out wp.Magazine); break;
                            case nameof(wp.Price): int.TryParse(data[i][j], out wp.Price); break;
                            case nameof(wp.Tag): ItemTag.TryParse(data[i][j], out wp.Tag); break;
                            case nameof(wp.Type): WeaponType.TryParse(data[i][j], out wp.Type); break;
                        }
                    }

                    Weapons.Add(wp);
                }

                OnSuccess?.Invoke(data);
            }, OnFail);
    }
    public List<List<string>> GetCsvData()
    {
        List<List<string>> result = new List<List<string>>();
        WeaponDetailData wp = Weapons[0];
        //tittle
        result.Add(new List<string>()
        {
           nameof(wp.ID),
           nameof(wp.Name),
           nameof(wp.Type),
           nameof(wp.Damage),
           nameof(wp.HeadshotDamage),
           nameof(wp.FireRate),
           nameof(wp.Magazine),
           nameof(wp.Price),
           nameof(wp.Tag),
        });
        //data  
        for (int i = 1; i <= Weapons.Count; i++)
        {
            result.Add(new List<string>()
            {
                Weapons[i - 1].ID.ToString(),
                Weapons[i - 1].Name.ToString(),
                Weapons[i - 1].Type.ToString(),
                Weapons[i - 1].Damage.ToString(),
                Weapons[i - 1].HeadshotDamage.ToString(),
                Weapons[i - 1].FireRate.ToString(),
                Weapons[i - 1].Magazine.ToString(),
                Weapons[i - 1].Price.ToString(),
                Weapons[i - 1].Tag.ToString(),
            });
        }

        return result;
    }
}

[Serializable]
public class WeaponDetailData
{
    [Header("General Information")]
    public int ID;
    public string Name;
    public WeaponType Type;
    public ItemTag Tag;

    [Header("Detail")]
    public int Damage;
    public int HeadshotDamage;
    public int FireRate;
    public int Magazine;
    public int Price;

}

public enum WeaponType
{
    Melee,
    One_Hand_Gun,
    Two_Hand_Gun,
}


#if UNITY_EDITOR

[CustomEditor(typeof(WeaponData))]
public class WeaponDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WeaponData data = (WeaponData)target;
        EditorGUILayout.Space(5);
        // Nút "Change"
        if (GUILayout.Button("Download Data"))
        {
            Undo.RecordObject(data, "Change MyData ID");
            Debug.Log("Start Downloading.....");
            EditorUtility.SetDirty(data);
            data.LoadDataFromServer(data.S_ID, data.G_ID, (data) =>
            {
                Debug.Log("Download Complete");
                AssetDatabase.SaveAssets();

            },
            () =>
            {
                Debug.Log("Download fail");
            });

        }
        EditorGUILayout.Space(5);

        DrawDefaultInspector();

    }
}
#endif