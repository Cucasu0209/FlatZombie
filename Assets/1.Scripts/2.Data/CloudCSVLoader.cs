using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloudCSVLoader : MonoBehaviour
{
    // Gán đúng ID và gid của từng sheet
    string sheetID = "1Sj_jaTKhZGuY97XICBNCHPUvm4gycBR7Gg1HBZspkXk";
    string gid_skin_data = "0";
    string gid_weapon_data = "118954083";
    string gid_zombie_data = "1089931934";
    string gid_level_data = "1549739825";

    void Start()
    {
        StartCoroutine(LoadSheet(gid_skin_data));
        StartCoroutine(LoadSheet(gid_weapon_data));
        StartCoroutine(LoadSheet(gid_zombie_data));
        StartCoroutine(LoadSheet(gid_level_data));
    }

    IEnumerator LoadSheet(string gid, Action<List<List<string>>> OnLoadComplete = null, Action OnLoadFail = null)
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
}