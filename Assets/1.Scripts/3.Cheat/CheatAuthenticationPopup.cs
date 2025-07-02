using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatAuthenticationPopup : MonoBehaviour
{
    [SerializeField] private Image Dark;
    [SerializeField] private Button ConfirmButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private TMP_InputField PasswordField;
    [SerializeField] private TextMeshProUGUI Warning;
    void Start()
    {
        SetupStart();
        CloseButton.onClick.AddListener(ClosePopup);
        ConfirmButton.onClick.AddListener(OnConfirmButtonClick);
        CheatManager.Instance.OnOpenCheatPopup += CheckAndOpen;
    }

    private void OnDestroy()
    {
        CheatManager.Instance.OnOpenCheatPopup -= CheckAndOpen;
    }

    private void CheckAndOpen()
    {
        if (CheatManager.Instance.IsAuthenticationPass() == false)
        {
            OpenPopup();
        }
    }
    private void SetupStart()
    {
        Dark.rectTransform.anchoredPosition = Vector2.zero;
        Dark.gameObject.SetActive(false);
        Warning.DOFade(0, 0.01f);
    }
    private void OpenPopup()
    {
        Dark.gameObject.SetActive(true);
    }
    private void ClosePopup()
    {
        Dark.gameObject.SetActive(false);
    }
    private void OnConfirmButtonClick()
    {
        string pw = PasswordField.text;
        if (CheatManager.Instance.IsPasswordCorrect(pw))
        {
            CheatManager.Instance.SavePassword(pw);
            ClosePopup();
            CheatManager.Instance.OnOpenCheatPopup?.Invoke();
        }
        else
        {
            Warning.DOKill();
            Warning.DOFade(1, 0.1f);
            Warning.DOFade(0, 2f).SetDelay(1);

        }
    }
}
