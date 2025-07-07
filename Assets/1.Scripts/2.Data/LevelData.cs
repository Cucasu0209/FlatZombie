using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
#endif
[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public string S_ID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    public string G_ID = "1549739825";
    public List<LevelDetailData> Levels;

    public async void LoadDataFromServer(string newSID, string newGID, Action<List<List<string>>> OnSuccess = null, Action OnFail = null)
    {
        S_ID = newSID;
        G_ID = newGID;
        await CloudCSVLoader.LoadSheetAsync(S_ID, G_ID,
            (data) =>
            {
                Dictionary<int, LevelDetailData> levelDict = new Dictionary<int, LevelDetailData>();

                for (int i = 1; i < data.Count; i++)
                {

                    int level = 0;
                    for (int j = 0; j < data[i].Count; j++)
                    {
                        switch (data[0][j].Trim())
                        {
                            case "Level": int.TryParse(data[i][j].Trim(), out level); break;
                        }
                    }

                    if (levelDict.ContainsKey(level) == false)
                    {
                        LevelDetailData lv = new LevelDetailData();
                        WaveDetailData wave = new WaveDetailData();
                        lv.Level = level;
                        lv.Waves = new List<WaveDetailData>();
                        lv.Waves.Add(wave);
                        lv.Waves[lv.Waves.Count - 1].Zombies = new List<int>()
                        {
                            0,0,0,0,0,0,0,
                        };


                        for (int j = 0; j < data[i].Count; j++)
                        {
                            switch (data[0][j].Trim())
                            {
                                case nameof(lv.ID): int.TryParse(data[i][j].Trim(), out lv.ID); break;
                                case nameof(lv.Map): MapType.TryParse(data[i][j].Trim(), out lv.Map); break;
                                case nameof(lv.Waves): int.TryParse(data[i][j].Trim(), out lv.Waves[lv.Waves.Count - 1].WaveIndex); break;

                            }
                            for (int zIndex = 0; zIndex < lv.Waves[lv.Waves.Count - 1].Zombies.Count; zIndex++)
                            {
                                if (data[0][j].Trim() == $"Z0{zIndex + 1}")
                                {
                                    lv.Waves[lv.Waves.Count - 1].Zombies[zIndex] = int.Parse(data[i][j].Trim());
                                    break;
                                }
                            }
                        }
                        levelDict.Add(level, lv);
                    }
                    else
                    {
                        LevelDetailData lv = levelDict[level];
                        WaveDetailData wave = new WaveDetailData();
                        lv.Waves.Add(wave);
                        lv.Waves[lv.Waves.Count - 1].Zombies = new List<int>()
                        {
                            0,0,0,0,0,0,0,
                        };


                        for (int j = 0; j < data[i].Count; j++)
                        {
                            switch (data[0][j].Trim())
                            {
                                case nameof(lv.Waves): int.TryParse(data[i][j].Trim(), out lv.Waves[lv.Waves.Count - 1].WaveIndex); break;
                            }
                            for (int zIndex = 0; zIndex < lv.Waves[lv.Waves.Count - 1].Zombies.Count; zIndex++)
                            {
                                if (data[0][j].Trim() == $"Z0{zIndex + 1}")
                                {
                                    lv.Waves[lv.Waves.Count - 1].Zombies[zIndex] = int.Parse(data[i][j].Trim());
                                    break;
                                }
                            }
                        }
                    }
                }
                Levels = new List<LevelDetailData>();
                foreach (var level in levelDict.Values) Levels.Add(level);
                OnSuccess?.Invoke(data);
            }, OnFail);
    }
    public List<List<string>> GetCsvData()
    {
        List<List<string>> result = new List<List<string>>();
        LevelDetailData z = Levels[0];
        //tittle
        result.Add(new List<string>()
        {
           nameof(z.ID),
           nameof(z.Map),
           nameof(z.Level),
           nameof(z.Waves),
        });
        for (int zIndex = 0; zIndex < Levels[0].Waves[0].Zombies.Count; zIndex++)
        {
            result[0].Add($"Z0{zIndex + 1}");
        }

        //data
        for (int i = 1; i <= Levels.Count; i++)
        {
            for (int w = 1; w <= Levels[i - 1].Waves.Count; w++)
            {
                List<string> l = new List<string>();
                l.Add(Levels[i - 1].ID.ToString());
                l.Add(Levels[i - 1].Map.ToString());
                l.Add(Levels[i - 1].Level.ToString());
                l.Add(Levels[i - 1].Waves[w - 1].ToString());
                for (int zIndex = 1; zIndex <= Levels[i - 1].Waves[w - 1].Zombies.Count; zIndex++)
                {
                    l.Add(Levels[i - 1].Waves[w - 1].Zombies[zIndex - 1].ToString());
                }
                result.Add(l);
            }
        }
        return result;

    }
}

[Serializable]
public class LevelDetailData
{
    public int ID;
    public MapType Map;
    public int Level;
    public List<WaveDetailData> Waves;
}
[Serializable]
public class WaveDetailData
{
    public int WaveIndex;
    public List<int> Zombies; //<id, Number of>
}

public enum MapType
{
    Bridge,
    Street,
    Playground
}

#if UNITY_EDITOR

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelData data = (LevelData)target;
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