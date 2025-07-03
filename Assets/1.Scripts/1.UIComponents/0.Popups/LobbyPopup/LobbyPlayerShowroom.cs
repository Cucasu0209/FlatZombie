using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerShowroom : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerInfor;
    [SerializeField] private Button NextSkinBtn;
    [SerializeField] private Button PreviousSkinBtn;
    private void Start()
    {
        NextSkinBtn.onClick.AddListener(OnNextBtnClick);
        PreviousSkinBtn.onClick.AddListener(OnPrevBtnClick);
        PlayerData.Instance.OnCurrentSkinUsedChange += UpdateState;
        PlayerData.Instance.OnCurrentWeaponUsedChange += UpdateState;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnCurrentSkinUsedChange -= UpdateState;
        PlayerData.Instance.OnCurrentWeaponUsedChange -= UpdateState;
    }
    public void UpdateState()
    {
        string weaponData = "Weapon:";
        WeaponData wp;
        for (int i = 0; i < PlayerData.Instance.CurrentWeaponsIdUsed.Count; i++)
        {
            wp = InventoryManager.Instance.GetWeaponDataById(PlayerData.Instance.CurrentWeaponsIdUsed[i]);
            if (wp != null)
                weaponData += $"\n -{wp.Name}";
            else weaponData += $"\n -Empty slot";
        }

        PlayerInfor.SetText($"Skin: \n{InventoryManager.Instance.GetSkinDataById(PlayerData.Instance.CurrentSkinIdUsed).Name}\n" + weaponData);
    }
    private void OnNextBtnClick()
    {
        PlayerData.Instance.ChangeSkin(PlayerData.Instance.GetNextSkinUnlocked());
    }
    private void OnPrevBtnClick()
    {
        PlayerData.Instance.ChangeSkin(PlayerData.Instance.GetPreviousSkinUnlocked());

    }
}
