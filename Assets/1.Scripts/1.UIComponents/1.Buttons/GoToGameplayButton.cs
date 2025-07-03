using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToGameplayButton : MonoBehaviour
{
    [SerializeField] private Button GoToMapBtn;
    private void Start()
    {
        GoToMapBtn.onClick.AddListener(OnGotoGameplayBtnClick);
    }
    private void OnGotoGameplayBtnClick()
    {
        Debug.LogError("LOAD SCENE GAMEPLAY");
    }
}
