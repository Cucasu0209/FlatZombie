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
        UpdateMoney();
        PlayerData.Instance.OnMoneyChange += UpdateMoney;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnMoneyChange -= UpdateMoney;

    }
    private void UpdateMoney()
    {
        MoneyField.text = PlayerData.Instance.CurrentMoney.ToString();
    }
    private void OnApplyClick()
    {
        if (int.TryParse(MoneyField.text, out int targetMoney))
        {
            PlayerData.Instance.CheatMoney(targetMoney);
        }
        UpdateMoney();
    }
}
