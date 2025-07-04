using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
#endif
[CreateAssetMenu(fileName = "SkinData", menuName = "Data/SkinData", order = 0)]
public class SkinData : ScriptableObject
{
    public string S_ID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    public string G_ID = "0";
    public List<SkinDetailData> Skins;
    public async void LoadDataFromServer(string newSID, string newGID, Action<List<List<string>>> OnSuccess = null, Action OnFail = null)
    {
        S_ID = newSID;
        G_ID = newGID;
        await CloudCSVLoader.LoadSheetAsync(S_ID, G_ID,
            (data) =>
            {
                Skins = new List<SkinDetailData>();
                for (int i = 1; i < data.Count; i++)
                {
                    SkinDetailData sk = new SkinDetailData();

                    for (int j = 0; j < data[i].Count; j++)
                    {
                        Debug.Log(data[0][j]);

                        switch (data[0][j].Trim())
                        {
                            case "ID": int.TryParse(data[i][j], out sk.ID); break;
                            case "Name": sk.Name = data[i][j]; break;
                            case "HP": int.TryParse(data[i][j], out sk.HP); break;
                            case "Price": int.TryParse(data[i][j], out sk.Price); break;
                            case "Tag": ItemTag.TryParse(data[i][j], out sk.Tag); break;
                        }
                    }

                    Skins.Add(sk);
                }

                OnSuccess?.Invoke(data);
            }, OnFail);
    }
}

[Serializable]
public class SkinDetailData
{
    public int ID;
    public string Name;
    public int HP;
    public int Price;
    public ItemTag Tag;
}

public enum ItemTag
{
    Default,
    WatchAds,
    BuyInShop
}

#if UNITY_EDITOR

[CustomEditor(typeof(SkinData))]
public class SkinDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SkinData data = (SkinData)target;
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