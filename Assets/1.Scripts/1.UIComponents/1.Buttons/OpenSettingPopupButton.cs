using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettingPopupButton : MonoBehaviour
{
    [SerializeField] private Button SettingButton;

    private void Start()
    {
        SettingButton.onClick.AddListener(OnSettingButtonClick);
    }
    private void OnDestroy()
    {
        
    }
    private void OnSettingButtonClick()
    {
        UIGlobalManager.Instance.OnOpenSettingPopup?.Invoke();
    }
}
