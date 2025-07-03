using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToMapButton : MonoBehaviour
{
    [SerializeField] private Button GoToMapBtn;
    private void Start()
    {
        GoToMapBtn.onClick.AddListener(OnGotoMapBtnClick);
    }
    private void OnGotoMapBtnClick()
    {
        UIGlobalManager.Instance.OnGoToMap?.Invoke();
    }
}
