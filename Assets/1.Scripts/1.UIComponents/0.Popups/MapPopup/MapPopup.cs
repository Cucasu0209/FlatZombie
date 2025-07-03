using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopup : MonoBehaviour
{
    [SerializeField] private RectTransform Content;
    [SerializeField] private MapGrid Grid;

    private void Start()
    {
        SetupStart();
        UIGlobalManager.Instance.OnGoToMap += OpenPopup;
    }
    private void OnDestroy()
    {
        UIGlobalManager.Instance.OnGoToMap -= OpenPopup;

    }
    private void SetupStart()
    {
        Content.anchoredPosition = Vector2.zero;
        Content.gameObject.SetActive(false);
    }
    private void OpenPopup()
    {
        Grid.UpdateGrid();
        Content.gameObject.SetActive(true);

    }
    private void ClosePopup()
    {
        Content.gameObject.SetActive(false);

    }
}
