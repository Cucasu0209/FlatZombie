using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatDataPopup : MonoBehaviour
{
    [Header("InputFields")]
    [SerializeField] private TMP_InputField SheetIDField;
    [SerializeField] private TMP_InputField SkinIDField;
    [SerializeField] private TMP_InputField WeaponIDField;
    [SerializeField] private TMP_InputField ZombieIDField;
    [SerializeField] private TMP_InputField LevelIDField;

    [Header("Download Buttons")]
    [SerializeField] private Button DownloadSkinDataButton;
    [SerializeField] private Button DownloadWeaponDataButton;
    [SerializeField] private Button DownloadZombieDataButton;
    [SerializeField] private Button DownloadLevelDataButton;

    [Header("Show Buttons")]
    [SerializeField] private Button ShowSkinDataButton;
    [SerializeField] private Button ShowWeaponDataButton;
    [SerializeField] private Button ShowZombieDataButton;
    [SerializeField] private Button ShowLevelDataButton;

    [Header("Others")]
    [SerializeField] private RectTransform PopupBody;
    [SerializeField] private TextMeshProUGUI StateTxt;
    [SerializeField] private CheatTableData TableData;
    [SerializeField] private Button CloseButton;

    private void Start()
    {
        SetupStart();
        DownloadSkinDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;

            InventoryManager.Instance.DownloadSkinData(SheetIDField.text, SkinIDField.text, OnDownloadComplete, OnDownloadFail);
        });
        DownloadWeaponDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            InventoryManager.Instance.DownloadWeaponData(SheetIDField.text, WeaponIDField.text, OnDownloadComplete, OnDownloadFail);

        });
        DownloadZombieDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            EnemyManager.Instance.DownloadZombieData(SheetIDField.text, ZombieIDField.text, OnDownloadComplete, OnDownloadFail);

        });
        DownloadLevelDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            LevelManager.Instance.DownloadLevelData(SheetIDField.text, LevelIDField.text, OnDownloadComplete, OnDownloadFail);

        });
        CloseButton.onClick.AddListener(ClosePopup);
        CheatManager.Instance.OnOpenCheatDataPopup += OpenPopup;
    }
    private void OnDestroy()
    {
        CheatManager.Instance.OnOpenCheatDataPopup -= OpenPopup;

    }
    private void SetupStart()
    {
        PopupBody.anchoredPosition = Vector2.zero;
        PopupBody.gameObject.SetActive(false);

    }
    private void OpenPopup()
    {
        SheetIDField.SetTextWithoutNotify(InventoryManager.Instance.SkinData.S_ID);
        SkinIDField.SetTextWithoutNotify(InventoryManager.Instance.SkinData.G_ID);
        WeaponIDField.SetTextWithoutNotify(InventoryManager.Instance.WeaponData.G_ID);
        ZombieIDField.SetTextWithoutNotify(EnemyManager.Instance.ZombieData.G_ID);
        LevelIDField.SetTextWithoutNotify(LevelManager.Instance.LevelData.G_ID);
        PopupBody.gameObject.SetActive(true);
    }
    private void ClosePopup()
    {
        PopupBody.gameObject.SetActive(false);

    }

    private void ShowData(List<List<string>> data)
    {
        TableData.SetData(data);
    }
    private void OnDownloadComplete(List<List<string>> data)
    {
        StateTxt.SetText("Download Success!!!");
        StateTxt.color = Color.green;
        ShowData(data);
    }
    private void OnDownloadFail()
    {
        StateTxt.SetText("Download Fail!!!");
        StateTxt.color = Color.red;
    }

}
