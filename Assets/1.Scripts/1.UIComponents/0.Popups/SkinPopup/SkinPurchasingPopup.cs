using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPurchasingPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Button CloseButton;
    [SerializeField] private RectTransform PopupContent;

    [Header("Content")]
    [SerializeField] private SkinGrid Grid;

    private void Start()
    {
        SetupStart();
        CloseButton.onClick.AddListener(HidePopup);
        UIGlobalManager.Instance.OnOpenSkinPopup += OpenPopup;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnOpenSkinPopup -= OpenPopup;
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
        Grid.UpdateSkinList();
    }
}