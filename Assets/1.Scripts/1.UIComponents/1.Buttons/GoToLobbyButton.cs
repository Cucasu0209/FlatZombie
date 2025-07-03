using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToLobbyButton : MonoBehaviour
{
    [SerializeField] private Button GoToMapBtn;
    private void Start()
    {
        GoToMapBtn.onClick.AddListener(OnGotoLobbyBtnClick);
    }
    private void OnGotoLobbyBtnClick()
    {
        UIGlobalManager.Instance.OnGoToLobby?.Invoke();
    }
}
