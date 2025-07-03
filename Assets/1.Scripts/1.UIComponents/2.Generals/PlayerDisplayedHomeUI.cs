using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplayedHomeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SkinIndex;
    [SerializeField] private TextMeshProUGUI WeaponIndex;
    private void Start()
    {
        UpdateSkin();
        UpdateWeapon();
        PlayerData.Instance.OnCurrentSkinUsedChange += UpdateSkin;
        PlayerData.Instance.OnCurrentWeaponUsedChange += UpdateWeapon;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnCurrentSkinUsedChange -= UpdateSkin;
        PlayerData.Instance.OnCurrentWeaponUsedChange -= UpdateWeapon;

    }

    private void UpdateSkin()
    {
        SkinIndex.SetText($"Skin\n {InventoryManager.Instance.GetSkinDataById(PlayerData.Instance.CurrentSkinIdUsed).Name}");
    }
    private void UpdateWeapon()
    {
        string weaponString = "";
        for (int i = 0; i < PlayerData.Instance.CurrentWeaponsIdUsed.Count; i++)
        {
            weaponString += "\n- " + InventoryManager.Instance.GetWeaponDataById(PlayerData.Instance.CurrentWeaponsIdUsed[i]).Name;
        }
        WeaponIndex.SetText("Weapon: " + weaponString);
    }
}
