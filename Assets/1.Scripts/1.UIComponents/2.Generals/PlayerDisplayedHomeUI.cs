using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayedHomeUI : MonoBehaviour
{
    [SerializeField] private Image SkinDisplayed;
    [SerializeField] private Image WeaponDisplayed;
    [SerializeField] private TextMeshProUGUI SkinIndex;
    [SerializeField] private TextMeshProUGUI WeaponIndex;
    private void Start()
    {
        UpdateSkin();
        UpdateWeapon();
        PlayerData.Instance.OnCurrentSkinUsedChange += UpdateSkin;
        PlayerData.Instance.OnCurrentWeaponUsedChange += UpdateWeapon;
        UIGlobalManager.Instance.OnGoToLobby += HideZone;
        UIGlobalManager.Instance.OnExitLobby += ShowZone;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnCurrentSkinUsedChange -= UpdateSkin;
        PlayerData.Instance.OnCurrentWeaponUsedChange -= UpdateWeapon;
        UIGlobalManager.Instance.OnGoToLobby -= HideZone;
        UIGlobalManager.Instance.OnExitLobby -= ShowZone;
    }

    private void HideZone()
    {
        SkinDisplayed.gameObject.SetActive(false);
        WeaponDisplayed.gameObject.SetActive(false);
    }
    private void ShowZone()
    {
        SkinDisplayed.gameObject.SetActive(true);
        WeaponDisplayed.gameObject.SetActive(true);
    }
    private void UpdateSkin()
    {
        SkinIndex.SetText($"Skin\n {InventoryManager.Instance.GetSkinDataById(PlayerData.Instance.CurrentSkinIdUsed).Name}");
    }
    private void UpdateWeapon()
    {
        string weaponString = "";
        WeaponDetailData wp;

        for (int i = 0; i < PlayerData.Instance.CurrentWeaponsIdUsed.Count; i++)
        {
            wp = InventoryManager.Instance.GetWeaponDataById(PlayerData.Instance.CurrentWeaponsIdUsed[i]);
            if (wp != null)
                weaponString += $"\n -{wp.Name}";
            else weaponString += $"\n -Empty slot";
        }
        WeaponIndex.SetText("Weapon: " + weaponString);
    }
}
