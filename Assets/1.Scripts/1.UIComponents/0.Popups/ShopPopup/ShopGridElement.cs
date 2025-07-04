using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopGridElement : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private TextMeshProUGUI WeaponId;
    [SerializeField] private TextMeshProUGUI WeaponName;
    [SerializeField] private TextMeshProUGUI Price;

    [Header("State")]
    [SerializeField] private Image SelectedBackground;
    [SerializeField] private Button BuyButton;
    [SerializeField] private Button WatchButton;
    [SerializeField] private TextMeshProUGUI UseMark;
    [SerializeField] private TextMeshProUGUI UsedMark;
    [SerializeField] private Button SelectButton;

    private WeaponDetailData CurrentData;

    private void Start()
    {
        BuyButton.onClick.AddListener(OnBuyButtonClick);
        SelectButton.onClick.AddListener(OnSelectButtonClick);
        WatchButton.onClick.AddListener(OnWatchButtonClick);
        InventoryManager.Instance.OnSelectWeapon += OnOneWeaponSelected;
        PlayerData.Instance.OnCurrentWeaponUsedChange += OnOneWeaponUsed;
        PlayerData.Instance.OnUnlockNewWeapon += UpdateState;

    }
    private void OnDestroy()
    {
        InventoryManager.Instance.OnSelectWeapon -= OnOneWeaponSelected;
        PlayerData.Instance.OnCurrentWeaponUsedChange -= OnOneWeaponUsed;
        PlayerData.Instance.OnUnlockNewWeapon -= UpdateState;
    }
    public void SetData(WeaponDetailData data)
    {
        CurrentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        WeaponId.SetText(CurrentData.ID.ToString());
        WeaponName.SetText(CurrentData.Name);
        Price.SetText(CurrentData.Price.ToString());

        bool haveWeapon = PlayerData.Instance.HaveWeapon(CurrentData.ID);
        bool isUsed = PlayerData.Instance.CurrentWeaponsIdUsed.Contains(CurrentData.ID);
        BuyButton.gameObject.SetActive(haveWeapon == false && CurrentData.Tag == ItemTag.BuyInShop);
        WatchButton.gameObject.SetActive(haveWeapon == false && CurrentData.Tag == ItemTag.WatchAds);
        BuyButton.gameObject.SetActive(haveWeapon == false);
        UseMark.gameObject.SetActive(haveWeapon && isUsed == false);
        UsedMark.gameObject.SetActive(haveWeapon && isUsed);
        SelectedBackground.gameObject.SetActive(CurrentData.ID == InventoryManager.Instance.CurrentWeaponIdSelected);
    }

    private void OnSelectButtonClick()
    {
        InventoryManager.Instance.SelectWeapon(CurrentData.ID);
        UIGlobalManager.Instance.OnShowWeaponInfoPopup?.Invoke(CurrentData.ID);
    }
    private void OnBuyButtonClick()
    {
        InventoryManager.Instance.RequestBuyWeapon(CurrentData.ID);

    }
    private void OnWatchButtonClick()
    {
        Debug.LogError("SHOW ADS");
        InventoryManager.Instance.SelectWeapon(CurrentData.ID);
        PlayerData.Instance.UnlockWeapon(CurrentData.ID);
        UIGlobalManager.Instance.OnOpenSelectWeaponSlotPopup((slotIndex) =>
        {
            PlayerData.Instance.ChangeWeapon(CurrentData.ID, slotIndex);
        },
        () =>
        {

        });
    }
    private void OnOneWeaponSelected()
    {
        UpdateState();
    }
    private void OnOneWeaponUsed()
    {
        UpdateState();
    }
}
