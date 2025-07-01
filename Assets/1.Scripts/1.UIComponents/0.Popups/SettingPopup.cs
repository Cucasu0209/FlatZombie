using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Image Dark;
    [SerializeField] private Button CloseButton;
    [SerializeField] private RectTransform PopupContent;

    [Header("Content")]
    [SerializeField] private Button HapticButton;
    [SerializeField] private Button SoundFxButton;
    [SerializeField] private Button MusicButton;


    private void Start()
    {
        SetupStart();
        HapticButton.onClick.AddListener(ToggleHaptic);
        SoundFxButton.onClick.AddListener(ToggleSoundFx);
        MusicButton.onClick.AddListener(ToggleMusic);
        CloseButton.onClick.AddListener(HidePopup);
        UIGlobalManager.Instance.OnOpenSettingPopup += OpenPopup;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnOpenSettingPopup -= OpenPopup;

    }
    private void SetupStart()
    {
        PopupContent.anchoredPosition = Vector2.zero;
        Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, 0.8f);
        Dark.gameObject.SetActive(false);
        CloseButton.gameObject.SetActive(false);
        PopupContent.gameObject.SetActive(false);
    }

    private void OpenPopup()
    {
        Dark.gameObject.SetActive(true);
        CloseButton.gameObject.SetActive(true);
        PopupContent.gameObject.SetActive(true);
        UpdateState();
    }
    private void HidePopup()
    {
        Dark.gameObject.SetActive(false);
        CloseButton.gameObject.SetActive(false);
        PopupContent.gameObject.SetActive(false);
    }
    private void UpdateState()
    {
        HapticButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Haptic\n{(UserData.GetHapticState() ? "On" : "Off")}");
        SoundFxButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"SFX\n{(UserData.GetSoundFxState() ? "On" : "Off")}");
        MusicButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Music\n{(UserData.GetMusicState() ? "On" : "Off")}");
    }
    private void ToggleHaptic()
    {
        SoundManager.Instance.SetHapticState(!UserData.GetHapticState());
        UpdateState();
    }
    private void ToggleMusic()
    {
        SoundManager.Instance.SetBGMusicState(!UserData.GetMusicState());
        UpdateState();
    }
    private void ToggleSoundFx()
    {
        SoundManager.Instance.SetSoundFxState(!UserData.GetSoundFxState());
        UpdateState();
    }
}
