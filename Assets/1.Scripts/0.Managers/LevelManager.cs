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
    public MapData CurrentMapData { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    public Dictionary<int, LevelData> LevelDictionary { get; private set; }

    #endregion

    #region Events;
    public Action OnCurrentLevelChange;
    #endregion

    private void LoadLevelsData()
    {
        CurrentMapData = Resources.Load<MapData>(GameConfig.MAP_DATA_LINK);
        CurrentLevelIndex = CurrentMapData.Levels[0].Index;
        LevelDictionary = new Dictionary<int, LevelData>();
        for (int i = 0; i < CurrentMapData.Levels.Count; i++)
        {
            if (LevelDictionary.ContainsKey(CurrentMapData.Levels[i].Index) == false)
            {
                LevelDictionary.Add(CurrentMapData.Levels[i].Index, CurrentMapData.Levels[i]);
            }
        }
    }
    public void ChangeLevel(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
        OnCurrentLevelChange?.Invoke();
    }
}
