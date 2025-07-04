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
        DownloadSkinDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            CloudCSVLoader.Instance.SetSheetID(SheetIDField.text);
            CloudCSVLoader.Instance.SetGID(CloudCSVLoader.DataType.Skin, SkinIDField.text);
            CloudCSVLoader.Instance.LoadData(CloudCSVLoader.DataType.Skin, OnDownloadComplete, OnDownloadFail);
        });
        DownloadWeaponDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            CloudCSVLoader.Instance.SetSheetID(SheetIDField.text);
            CloudCSVLoader.Instance.SetGID(CloudCSVLoader.DataType.Weapon, WeaponIDField.text);
            CloudCSVLoader.Instance.LoadData(CloudCSVLoader.DataType.Weapon, OnDownloadComplete, OnDownloadFail);
        });
        DownloadZombieDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            CloudCSVLoader.Instance.SetSheetID(SheetIDField.text);
            CloudCSVLoader.Instance.SetGID(CloudCSVLoader.DataType.Zombie, ZombieIDField.text);
            CloudCSVLoader.Instance.LoadData(CloudCSVLoader.DataType.Zombie, OnDownloadComplete, OnDownloadFail);
        });
        DownloadLevelDataButton.onClick.AddListener(() =>
        {
            StateTxt.SetText("Downloading...");
            StateTxt.color = Color.white;
            CloudCSVLoader.Instance.SetSheetID(SheetIDField.text);
            CloudCSVLoader.Instance.SetGID(CloudCSVLoader.DataType.Level, LevelIDField.text);
            CloudCSVLoader.Instance.LoadData(CloudCSVLoader.DataType.Level, OnDownloadComplete, OnDownloadFail);
        });
        CloseButton.onClick.AddListener(ClosePopup);
        OpenPopup();
    }
    private void SetupStart()
    {
        PopupBody.anchoredPosition = Vector2.zero;
        PopupBody.gameObject.SetActive(false);

    }
    private void OpenPopup()
    {
        SheetIDField.SetTextWithoutNotify(CloudCSVLoader.Instance.GetSheetID());
        SkinIDField.SetTextWithoutNotify(CloudCSVLoader.Instance.GetGID(CloudCSVLoader.DataType.Skin));
        WeaponIDField.SetTextWithoutNotify(CloudCSVLoader.Instance.GetGID(CloudCSVLoader.DataType.Weapon));
        ZombieIDField.SetTextWithoutNotify(CloudCSVLoader.Instance.GetGID(CloudCSVLoader.DataType.Zombie));
        LevelIDField.SetTextWithoutNotify(CloudCSVLoader.Instance.GetGID(CloudCSVLoader.DataType.Level));
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
