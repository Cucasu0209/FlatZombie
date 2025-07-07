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
                        switch (data[0][j].Trim())
                        {
                            case nameof(sk.ID): int.TryParse(data[i][j].Trim(), out sk.ID); break;
                            case nameof(sk.Name): sk.Name = data[i][j].Trim(); break;
                            case nameof(sk.HP): int.TryParse(data[i][j].Trim(), out sk.HP); break;
                            case nameof(sk.Price): int.TryParse(data[i][j].Trim(), out sk.Price); break;
                            case nameof(sk.Tag): ItemTag.TryParse(data[i][j].Trim(), out sk.Tag); break;
                        }
                    }

                    Skins.Add(sk);
                }

                OnSuccess?.Invoke(data);
            }, OnFail);
    }
    public List<List<string>> GetCsvData()
    {
        List<List<string>> result = new List<List<string>>();
        SkinDetailData sk = Skins[0];
        //tittle
        result.Add(new List<string>()
        {
           nameof(sk.ID),
           nameof(sk.Name),
           nameof(sk.HP),
           nameof(sk.Price),
           nameof(sk.Tag),
        });
        //data  
        for (int i = 1; i <= Skins.Count; i++)
        {
            result.Add(new List<string>()
            {
                Skins[i - 1].ID.ToString(),
                Skins[i - 1].Name.ToString(),
                Skins[i - 1].HP.ToString(),
                Skins[i - 1].Price.ToString(),
                Skins[i - 1].Tag.ToString(),
            });
        }

        return result;
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