using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponSlotPopup : MonoBehaviour
{
    [SerializeField] private Image Dark;
    [SerializeField] private RectTransform SlotsHolder;
    [SerializeField] private WeaponSlotElement SlotPrefab;
    [SerializeField] private Button CancelButton;

    private List<WeaponSlotElement> Slots = new List<WeaponSlotElement>();
    private Action<int> OnChoseSlot;
    private Action OnCancel;
    private void Start()
    {
        SetupStart();
        CancelButton.onClick.AddListener(OnCancelClick);
        UIGlobalManager.Instance.OnOpenSelectWeaponSlotPopup += OnSelectWeapon;
    }

    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnOpenSelectWeaponSlotPopup -= OnSelectWeapon;

    }
    private void OnSelectWeapon(Action<int> onChoseSlot, Action onCancel)
    {
        OnChoseSlot = onChoseSlot;
        OnCancel = onCancel;
        OpenPopup();
    }
    private void SetupStart()
    {
        Dark.rectTransform.anchoredPosition = Vector3.zero;
        Dark.gameObject.SetActive(false);
    }
    private void OpenPopup()
    {
        Dark.gameObject.SetActive(true);
        UpdateState();
    }
    private void OnCancelClick()
    {
        OnCancel?.Invoke();
        ClosePopup();
    }
    private void ClosePopup()
    {
        Dark.gameObject.SetActive(false);
    }
    private void UpdateState()
    {
        List<int> slotsid = PlayerData.Instance.CurrentWeaponsIdUsed;
        List<WeaponDetailData> slotsData = slotsid.Select(id => InventoryManager.Instance.GetWeaponDataById(id)).ToList();

        for (int i = 0; i < slotsData.Count; i++)
        {
            if (Slots.Count <= i) Slots.Add(LeanPool.Spawn(SlotPrefab, SlotsHolder));
            Slots[i].SetData(slotsData[i], i, OnSelectSlot);
            Slots[i].transform.localScale = Vector3.one;
        }
    }
    private void OnSelectSlot(int slotIndex)
    {
        OnChoseSlot?.Invoke(slotIndex);
        ClosePopup();
    }

}
