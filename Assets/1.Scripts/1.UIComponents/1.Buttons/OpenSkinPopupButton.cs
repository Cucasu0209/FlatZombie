using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSkinPopupButton : MonoBehaviour
{
    [SerializeField] private Button SkinButton;

    private void Start()
    {
        SkinButton.onClick.AddListener(OnSkinButtonClick);
    }
    private void OnDestroy()
    {

    }
    private void OnSkinButtonClick()
    {
        UIGlobalManager.Instance.OnOpenSkinPopup?.Invoke();
    }
}
