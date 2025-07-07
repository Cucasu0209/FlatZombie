using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Action OnGoForward;
    public Action OnGoBack;
    public Action OnChangeToMeleeBtnClick;
    public Action OnFillBulletBtnClick;
    public Action OnSwitchGunClick;
    public Action<Vector2> OnTouchScreen; //vector dir

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnTouchScreen?.Invoke(Input.mousePosition);
        }
    }
}
