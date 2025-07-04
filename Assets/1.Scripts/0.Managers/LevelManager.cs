using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Simple Singleton
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
        LoadLevelsData();
    }
    #endregion

    #region Variables
    public LevelData LevelData { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    public Dictionary<int, LevelDetailData> LevelDictionary { get; private set; }

    #endregion

    #region Events
    public Action OnCurrentLevelChange;
    #endregion
    public void DownloadLevelData(string newSID, string newGID, Action<List<List<string>>> OnSuccess, Action OnFail)
    {
        LevelData.LoadDataFromServer(newSID, newGID, OnSuccess, OnFail);
    }
    private void LoadLevelsData()
    {
        LevelData = Resources.Load<LevelData>(GameConfig.LEVEL_DATA_LINK);
        CurrentLevelIndex = LevelData.Levels[0].Level;
        LevelDictionary = new Dictionary<int, LevelDetailData>();
        for (int i = 0; i < LevelData.Levels.Count; i++)
        {
            if (LevelDictionary.ContainsKey(LevelData.Levels[i].Level) == false)
            {
                LevelDictionary.Add(LevelData.Levels[i].Level, LevelData.Levels[i]);
            }
        }
    }
    public void ChangeLevel(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
        OnCurrentLevelChange?.Invoke();
    }
}
