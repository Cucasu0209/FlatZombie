using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    #region Simple Singleton
    public static CheatManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public Action OnOpenCheatPopup;
    public Action OnOpenCheatDataPopup;
    private bool IsDragging = false;
    private Vector2 MousePos;
    private List<Vector2> ScreenCorners;
    private List<bool> CheckInList;
    private float TimeReset = 10;
    private string CurrentPassword;

    private void Start()
    {
        LoadPassword();
        ScreenCorners = new List<Vector2>()
        {
            Camera.main.ScreenToWorldPoint(new Vector2(0, 0)),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)),
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height/2)),
        };
        CheckInList = new List<bool>()
        {
            false,
            false,
            false,
            false,
            false,
        };
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) IsDragging = true;
        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }

        if (IsDragging)
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckToOpenCheatPopup(MousePos);
        }
        TimeReset -= Time.deltaTime;
        if (TimeReset < 0)
        {
            ClearCheckInList();
            TimeReset = 10;
        }
    }

    private void CheckToOpenCheatPopup(Vector2 mousePos)
    {
        for (int i = 0; i < ScreenCorners.Count; i++)
        {
            if (Vector2.Distance(mousePos, ScreenCorners[i]) < 1.5f)
            {
                CheckInList[i] = true;
                TimeReset = 10;
            }
        }
        if (CheckTouch4Corner())
        {
            OnOpenCheatPopup?.Invoke();
            ClearCheckInList();
        }
    }
    private void ClearCheckInList()
    {
        for (int i = 0; i < CheckInList.Count; i++) CheckInList[i] = false;
    }
    private bool CheckTouch4Corner()
    {
        for (int i = 0; i < CheckInList.Count; i++) if (CheckInList[i] == false) return false;
        return true;

    }

    private void LoadPassword()
    {
        CurrentPassword = PlayerPrefs.GetString(GameConfig.CHEAT_PASSWORD_KEY, "");
    }
    public void SavePassword(string pw)
    {
        CurrentPassword = pw;
        PlayerPrefs.SetString(GameConfig.CHEAT_PASSWORD_KEY, CurrentPassword);

    }
    public bool IsPasswordCorrect(string pw)
    {
        return pw == GameConfig.CHEAT_CORRECT_PASSWORD;
    }
    public bool IsAuthenticationPass()
    {
        return CurrentPassword == GameConfig.CHEAT_CORRECT_PASSWORD;
    }
}
