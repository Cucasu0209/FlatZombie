using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatMoney : MonoBehaviour
{
    [SerializeField] private TMP_InputField MoneyField;
    [SerializeField] private Button ApplyButton;

    private void Start()
    {
        ApplyButton.onClick.AddListener(OnApplyClick);
    }

    private void OnApplyClick()
    {

    }
}
