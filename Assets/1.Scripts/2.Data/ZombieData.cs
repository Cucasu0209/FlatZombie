using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ZombieData", menuName = "Data/ZombieData", order = 0)]

public class ZombieData : ScriptableObject
{
    public string S_ID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    public string G_ID = "1089931934";
    public List<ZombieDetailData> Zombies;

    public async void LoadDataFromServer(string newSID, string newGID, Action<List<List<string>>> OnSuccess = null, Action OnFail = null)
    {
        S_ID = newSID;
        G_ID = newGID;
        await CloudCSVLoader.LoadSheetAsync(S_ID, G_ID,
            (data) =>
            {
                Zombies = new List<ZombieDetailData>();
                for (int i = 1; i < data.Count; i++)
                {
                    ZombieDetailData zb = new ZombieDetailData();

                    for (int j = 0; j < data[i].Count; j++)
                    {
                        switch (data[0][j].Trim())
                        {
                            case "ID": int.TryParse(data[i][j], out zb.ID); break;
                            case "Name": zb.Name = data[i][j]; break;
                            case "HP": int.TryParse(data[i][j], out zb.HP); break;
                            case "Speed": float.TryParse(data[i][j], out zb.Speed); break;
                            case "Damage": int.TryParse(data[i][j], out zb.Damage); break;
                            case "AttackSpeed": int.TryParse(data[i][j], out zb.AttackSpeed); break;
                            case "NickName": zb.NickName = data[i][j]; break;
                            case "Price": int.TryParse(data[i][j], out zb.Price); break;
                        }
                    }

                    Zombies.Add(zb);
                }

                OnSuccess?.Invoke(data);
            }, OnFail);
    }
}
[Serializable]
public class ZombieDetailData
{
    public int ID;
    public string Name;
    public int HP;
    public float Speed;
    public int Damage;
    public int AttackSpeed;
    public string NickName;
    public int Price;
}

#if UNITY_EDITOR

[CustomEditor(typeof(ZombieData))]
public class ZombieDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ZombieData data = (ZombieData)target;
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