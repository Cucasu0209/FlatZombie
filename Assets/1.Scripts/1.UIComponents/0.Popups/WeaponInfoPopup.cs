using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Image Dark;
    [SerializeField] private TextMeshProUGUI WeaponName;
    [SerializeField] private Button CloseBtn;
    [SerializeField] private Button BuyBtn;
    [SerializeField] private Button WatchBtn;
    [SerializeField] private TextMeshProUGUI PriceTxt;
    [SerializeField] private Button UseBtn;
    [SerializeField] private Button EliminateBtn;

    [Header("Damage")]
    [SerializeField] private Slider Damage;
    [SerializeField] private TextMeshProUGUI DamageTxt;

    [Header("FireRate")]
    [SerializeField] private Slider FireRate;
    [SerializeField] private TextMeshProUGUI FireRateTxt;

    [Header("Magazine")]
    [SerializeField] private Slider Magazine;
    [SerializeField] private TextMeshProUGUI MagazineTxt;

    private WeaponData CurrentData;
    private void Start()
    {
        SetupStart();
        CloseBtn.onClick.AddListener(ClosePopup);
        BuyBtn.onClick.AddListener(OnBuyBtnClick);
        WatchBtn.onClick.AddListener(OnWatchBtnClick);
        UseBtn.onClick.AddListener(OnUseBtnClick);
        EliminateBtn.onClick.AddListener(OnEliminateBtnClick);
        UIGlobalManager.Instance.OnShowWeaponInfoPopup += ShowPopupWithWeaponID;
        PlayerData.Instance.OnCurrentWeaponUsedChange += UpdateState;
        PlayerData.Instance.OnUnlockNewWeapon += UpdateState;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnShowWeaponInfoPopup -= ShowPopupWithWeaponID;
        PlayerData.Instance.OnCurrentWeaponUsedChange -= UpdateState;
        PlayerData.Instance.OnUnlockNewWeapon -= UpdateState;
    }
    private void ShowPopupWithWeaponID(int weaponId)
    {
        SetData(InventoryManager.Instance.GetWeaponDataById(weaponId));
        OpenPopup();
    }
    private void SetData(WeaponData data)
    {
        CurrentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        if (CurrentData is null) return;
        WeaponName.text = $"Weapon {CurrentData.ID}: {CurrentData.Name}";

        Damage.value = CurrentData.Damage / GameConfig.WEAPON_DAMAGE_MAX;
        DamageTxt.text = CurrentData.Damage.ToString();

        FireRate.value = CurrentData.FireRate / GameConfig.WEAPON_FIRERATE_MAX;
        FireRateTxt.text = CurrentData.FireRate.ToString();

        if (CurrentData.Magazine > 0)
        {
            Magazine.value = 1f * CurrentData.Magazine / GameConfig.WEAPON_MAGAZINE_MAX;
            MagazineTxt.text = CurrentData.Magazine.ToString();
        }
        else
        {
            Magazine.value = 1;
            MagazineTxt.text = "Infinity";
        }



        PriceTxt.text = CurrentData.Price == 0 ? "Free" : CurrentData.Price.ToString();

        bool haveWeapon = PlayerData.Instance.HaveWeapon(CurrentData.ID);
        bool isUsed = PlayerData.Instance.CurrentWeaponsIdUsed.Contains(CurrentData.ID);
        BuyBtn.gameObject.SetActive(haveWeapon == false && CurrentData.Tag == ItemTag.BuyInShop);
        WatchBtn.gameObject.SetActive(haveWeapon == false && CurrentData.Tag == ItemTag.WatchAds);

        UseBtn.gameObject.SetActive(haveWeapon && isUsed == false);
        EliminateBtn.gameObject.SetActive(haveWeapon && isUsed);
    }
    private void SetupStart()
    {
        Dark.rectTransform.anchoredPosition = Vector3.zero;
        Dark.gameObject.SetActive(false);
    }
    private void OpenPopup()
    {
        Dark.gameObject.SetActive(true);
    }
    private void ClosePopup()
    {
        Dark.gameObject.SetActive(false);
    }
    private void OnBuyBtnClick()
    {
        InventoryManager.Instance.RequestBuyWeapon(CurrentData.ID);
    }
    private void OnWatchBtnClick()
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
    private void OnUseBtnClick()
    {
        if (PlayerData.Instance.HaveWeapon(CurrentData.ID))
        {
            UIGlobalManager.Instance.OnOpenSelectWeaponSlotPopup((slotIndex) =>
            {
                PlayerData.Instance.ChangeWeapon(CurrentData.ID, slotIndex);
            },
            () =>
            {
                //cancel
            });
        }
    }
    private void OnEliminateBtnClick()
    {
        PlayerData.Instance.EliminateWeapon(CurrentData.ID);
    }

}
