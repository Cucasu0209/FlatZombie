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
        SkinIndex.SetText($"Skin {InventoryManager.Instance.GetSkinDataById(PlayerData.Instance.CurrentSkinIdUsed).Name}");
    }
    private void UpdateWeapon()
    {
        SkinIndex.SetText($"Weapon {InventoryManager.Instance.GetSkinDataById(PlayerData.Instance.CurrentSkinIdUsed).Name}");
    }
}
