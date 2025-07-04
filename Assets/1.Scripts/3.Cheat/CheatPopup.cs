using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatPopup : MonoBehaviour
{
    [SerializeField] private Image Dark;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button OpenDataTable;

    void Start()
    {
        SetupStart();
        CloseButton.onClick.AddListener(ClosePopup);
        OpenDataTable.onClick.AddListener(() => CheatManager.Instance.OnOpenCheatDataPopup?.Invoke());

        CheatManager.Instance.OnOpenCheatPopup += CheckAndOpen;
    }

    private void OnDestroy()
    {
        CheatManager.Instance.OnOpenCheatPopup -= CheckAndOpen;
    }

    private void CheckAndOpen()
    {
        if (CheatManager.Instance.IsAuthenticationPass())
        {
            OpenPopup();
        }
    }
    private void SetupStart()
    {
        Dark.rectTransform.anchoredPosition = Vector2.zero;
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
}
