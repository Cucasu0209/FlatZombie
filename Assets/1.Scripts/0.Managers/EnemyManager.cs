using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Simple Singleton
    public static EnemyManager Instance;
    private void Awake()
    {
        Instance = this;
        LoadZombieData();
    }
    #endregion
    public ZombieData ZombieData { get; private set; }

    public void DownloadZombieData(string newSID, string newGID, Action<List<List<string>>> OnSuccess, Action OnFail)
    {
        ZombieData.LoadDataFromServer(newSID, newGID, OnSuccess, OnFail);
    }
    private void LoadZombieData()
    {
        if (ZombieData is null)
        {
            ZombieData = Resources.Load<ZombieData>(GameConfig.ZOMBIE_DATA_LINK);
        }
    }
}
