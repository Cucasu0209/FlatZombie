using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CloudCSVLoader
{
    public static async Task LoadSheetAsync(string sheetID, string gid,
       Action<List<List<string>>> OnLoadComplete = null,
       Action OnLoadFail = null)
    {
        string url = $"https://docs.google.com/spreadsheets/d/{sheetID}/export?format=csv&gid={gid}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield(); // Cho phép tiếp tục ở frame sau

            if (www.result == UnityWebRequest.Result.Success)
            {
                string text = www.downloadHandler.text;
                List<List<string>> result = new List<List<string>>();
                string[] lines = text.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    List<string> row = new List<string>(values);
                    result.Add(row);
                }

                OnLoadComplete?.Invoke(result);
            }
            else
            {
                Debug.LogError("Error loading sheet: " + www.error);
                OnLoadFail?.Invoke();
            }
        }
    }


}