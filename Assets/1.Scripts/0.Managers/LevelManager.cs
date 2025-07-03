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
    public LevelData CurrentLevelData { get; private set; }
    #endregion

    #region Events;

    #endregion

    private void LoadLevelsData()
    {
        CurrentMapData = Resources.Load<MapData>(GameConfig.SHOP_MAP_LINK);
    }
}
