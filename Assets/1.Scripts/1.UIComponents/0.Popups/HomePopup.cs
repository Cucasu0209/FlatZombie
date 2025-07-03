using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePopup : MonoBehaviour
{
    [SerializeField] private RectTransform Content;

    private void Start()
    {
        UIGlobalManager.Instance.OnGoToHome += OpenPopup;
        UIGlobalManager.Instance.OnExitHome += ClosePopup;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnGoToHome -= OpenPopup;
        UIGlobalManager.Instance.OnExitHome -= ClosePopup;

    }
    private void SetupStart()
    {
        Content.gameObject.SetActive(false);
        Content.anchoredPosition = Vector2.zero;
    }
    private void OpenPopup()
    {
        Content.gameObject.SetActive(true);

    }
    private void ClosePopup()
    {
        Content.gameObject.SetActive(false);
    }

}
