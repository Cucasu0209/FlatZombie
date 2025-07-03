using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToHomeButton : MonoBehaviour
{
    [SerializeField] private Button GoToMapBtn;
    private void Start()
    {
        GoToMapBtn.onClick.AddListener(OnGotoHomeBtnClick);
    }
    private void OnGotoHomeBtnClick()
    {
        UIGlobalManager.Instance.GoToHome();
    }
}
