using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyZone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    private void Start()
    {
        UpdateMoney();
        PlayerData.Instance.OnMoneyChange += UpdateMoney;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnMoneyChange -= UpdateMoney;

    }

    public void UpdateMoney()
    {
        MoneyText.SetText(PlayerData.Instance.CurrentMoney.ToString());
    }
}
