using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPopup : MonoBehaviour
{
    [SerializeField] private RectTransform Content;
    [SerializeField] private ShopGrid WeaponGrid;
    [SerializeField] private LobbyPlayerShowroom PlayerSR;

    private void Start()
    {
        SetupStart();
        UIGlobalManager.Instance.OnGoToLobby += OpenPopup;
        UIGlobalManager.Instance.OnExitLobby += ClosePopup;

    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnGoToLobby -= OpenPopup;
        UIGlobalManager.Instance.OnExitLobby -= ClosePopup;

    }

    private void SetupStart()
    {
        Content.anchoredPosition = Vector2.zero;
        Content.gameObject.SetActive(false);
    }
    private void OpenPopup()
    {
        Content.gameObject.SetActive(true);
        WeaponGrid.UpdateWeaponList();
        PlayerSR.UpdateState();
    }
    private void ClosePopup()
    {
        Content.gameObject.SetActive(false);

    }
}
