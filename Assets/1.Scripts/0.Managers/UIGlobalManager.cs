using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlobalManager : MonoBehaviour
{
    #region Simple singleton
    public static UIGlobalManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Events
    public Action OnOpenSettingPopup;
    public Action OnOpenSkinPopup;
    public Action OnOpenShopPopup;
    public Action<int> OnShowWeaponInfoPopup; // WeaponID
    public Action<Action<int>, Action> OnOpenSelectWeaponSlotPopup; // <Action Choose slot Index, Action Cancel>

    public Action OnGoToHome;
    public Action OnExitHome;
    public Action OnGoToMap;
    public Action OnExitMap;
    public Action OnGoToLobby;
    public Action OnExitLobby;
    #endregion

    #region Public Methods
    public void GoToHome()
    {
        OnGoToHome?.Invoke();
        OnExitLobby?.Invoke();
        OnExitMap?.Invoke();
    }
    public void GoToMap()
    {
        OnGoToMap?.Invoke();
        OnExitHome?.Invoke();
        OnExitLobby?.Invoke();
    }
    public void GoToLobby()
    {
        OnGoToLobby?.Invoke();
        OnExitHome?.Invoke();
        OnExitMap?.Invoke();
    }
    #endregion
}
