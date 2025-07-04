using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloudCSVLoader : MonoBehaviour
{
    public static CloudCSVLoader Instance;
    private void Awake()
    {
        Instance = this;
    }
    public enum DataType
    {
        Skin,
        Weapon,
        Zombie,
        Level
    }
    private string sheetID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    private string gid_skin_data = "0";
    private string gid_weapon_data = "118954083";
    private string gid_zombie_data = "1089931934";
    private string gid_level_data = "1549739825";

    private IEnumerator LoadSheet(string gid, Action<List<List<string>>> OnLoadComplete = null, Action OnLoadFail = null)
    {
        string url = $"https://docs.google.com/spreadsheets/d/{sheetID}/export?format=csv&gid={gid}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string text = www.downloadHandler.text;
            List<List<string>> result = new List<List<string>>();
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                result.Add(new List<string>());
                string[] values = lines[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    result[i].Add(values[j]);
                }
            }
            OnLoadComplete?.Invoke(result);
        }
        else
        {
            OnLoadFail?.Invoke();
            Debug.LogError("Error loading sheet: " + www.error);
        }
    }

    public void SetSheetID(string id) => sheetID = id;
    public void SetGID(DataType type, string gid)
    {
        switch (type)
        {
            case DataType.Skin:
                gid_skin_data = gid;
                break;
            case DataType.Level:
                gid_level_data = gid;
                break;
            case DataType.Weapon:
                gid_weapon_data = gid;
                break;
            case DataType.Zombie:
                gid_zombie_data = gid;
                break;
        }
    }
    public string GetSheetID() => sheetID;
    public string GetGID(DataType type)
    {
        switch (type)
        {
            case DataType.Skin:
                return gid_skin_data;
            case DataType.Level:
                return gid_level_data;
            case DataType.Weapon:
                return gid_weapon_data;
            case DataType.Zombie:
                return gid_zombie_data;
            default:
                return gid_zombie_data;
        }
    }
    public void LoadData(DataType type, Action<List<List<string>>> OnLoadComplete = null, Action OnLoadFail = null)
    {
        StartCoroutine(LoadSheet(GetGID(type), OnLoadComplete, OnLoadFail));
    }

}