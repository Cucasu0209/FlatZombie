using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotElement : MonoBehaviour
{
    [SerializeField] private Button SlotButton;
    [SerializeField] private TextMeshProUGUI WeaponName;
    private int SlotIndex;
    private Action<int> OnSelectSlot;
    private void Start()
    {
        SlotButton.onClick.AddListener(OnChoseSlot);
    }
    private void OnChoseSlot()
    {
        OnSelectSlot?.Invoke(SlotIndex);
    }
    public void SetData(WeaponData weapon, int slotIndex, Action<int> onSelectSlot)
    {
        SlotIndex = slotIndex;
        OnSelectSlot = onSelectSlot;
        if (weapon != null)
        {
            WeaponName.SetText(weapon.Name);
        }
        else
        {
            WeaponName.SetText("Empty");
        }
    }

}
