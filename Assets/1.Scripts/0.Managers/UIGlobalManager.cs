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
    #endregion

}
