using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopPopupButton : MonoBehaviour
{
    [SerializeField] private Button ShopButton;

    private void Start()
    {
        ShopButton.onClick.AddListener(OnShopButtonClick);
    }
    private void OnDestroy()
    {

    }
    private void OnShopButtonClick()
    {
        UIGlobalManager.Instance.OnOpenShopPopup?.Invoke();
    }
}
