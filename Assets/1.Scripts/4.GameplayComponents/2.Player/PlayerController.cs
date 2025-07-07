using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform GunPos;

    //cache
    private Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
        UserInput.Instance.OnTouchScreen += Fire;
    }
    private void OnDestroy()
    {
        UserInput.Instance.OnTouchScreen -= Fire;

    }

    private void Fire(Vector2 mousePos)
    {
        Vector2 wMouse = MainCamera.ScreenToWorldPoint(mousePos);
        Debug.Log(wMouse);
        if (wMouse.x > transform.position.x + 1)
        {
            //Fire
            float angle = -Vector2.SignedAngle(wMouse - (Vector2)GunPos.position, Vector2.right);
            Debug.Log(angle + " " + (wMouse - (Vector2)GunPos.position));


            GunPos.eulerAngles = Vector3.forward * angle;
        }
    }
}
