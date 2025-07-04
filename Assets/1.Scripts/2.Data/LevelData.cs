using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
                            case "Level": int.TryParse(data[i][j], out level); break;
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
                                case "ID": int.TryParse(data[i][j], out lv.ID); break;
                                case "Map": MapType.TryParse(data[i][j], out lv.Map); break;
                                case "Wave": int.TryParse(data[i][j], out lv.Waves[lv.Waves.Count - 1].WaveIndex); break;
                                case "Z01": lv.Waves[lv.Waves.Count - 1].Zombies[0] = int.Parse(data[i][j]); break;
                                case "Z02": lv.Waves[lv.Waves.Count - 1].Zombies[1] = int.Parse(data[i][j]); break;
                                case "Z03": lv.Waves[lv.Waves.Count - 1].Zombies[2] = int.Parse(data[i][j]); break;
                                case "Z04": lv.Waves[lv.Waves.Count - 1].Zombies[3] = int.Parse(data[i][j]); break;
                                case "Z05": lv.Waves[lv.Waves.Count - 1].Zombies[4] = int.Parse(data[i][j]); break;
                                case "Z06": lv.Waves[lv.Waves.Count - 1].Zombies[5] = int.Parse(data[i][j]); break;
                                case "Z07": lv.Waves[lv.Waves.Count - 1].Zombies[6] = int.Parse(data[i][j]); break;
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
                                case "Wave": int.TryParse(data[i][j], out lv.Waves[lv.Waves.Count - 1].WaveIndex); break;
                                case "Z01": lv.Waves[lv.Waves.Count - 1].Zombies[0] = int.Parse(data[i][j]); break;
                                case "Z02": lv.Waves[lv.Waves.Count - 1].Zombies[1] = int.Parse(data[i][j]); break;
                                case "Z03": lv.Waves[lv.Waves.Count - 1].Zombies[2] = int.Parse(data[i][j]); break;
                                case "Z04": lv.Waves[lv.Waves.Count - 1].Zombies[3] = int.Parse(data[i][j]); break;
                                case "Z05": lv.Waves[lv.Waves.Count - 1].Zombies[4] = int.Parse(data[i][j]); break;
                                case "Z06": lv.Waves[lv.Waves.Count - 1].Zombies[5] = int.Parse(data[i][j]); break;
                                case "Z07": lv.Waves[lv.Waves.Count - 1].Zombies[6] = int.Parse(data[i][j]); break;
                            }
                        }
                    }
                }
                Levels = new List<LevelDetailData>();
                foreach (var level in levelDict.Values) Levels.Add(level);
                OnSuccess?.Invoke(data);
            }, OnFail);
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