using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Button CloseButton;
    [SerializeField] private RectTransform PopupContent;

    private void Start()
    {
        SetupStart();
        CloseButton.onClick.AddListener(HidePopup);
        UIGlobalManager.Instance.OnOpenShopPopup += OpenPopup;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnOpenShopPopup -= OpenPopup;
    }

    private void SetupStart()
    {
        PopupContent.anchoredPosition = Vector2.zero;
        PopupContent.gameObject.SetActive(false);
    }

    private void OpenPopup()
    {
        PopupContent.gameObject.SetActive(true);
        UpdateState();
    }
    private void HidePopup()
    {
        PopupContent.gameObject.SetActive(false);
    }
    private void UpdateState()
    {

    }
}
