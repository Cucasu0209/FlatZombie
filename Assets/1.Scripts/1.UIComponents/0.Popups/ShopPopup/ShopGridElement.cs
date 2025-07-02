using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopGridElement : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private TextMeshProUGUI WeaponId;
    [SerializeField] private TextMeshProUGUI WeaponName;
    [SerializeField] private TextMeshProUGUI Price;

    [Header("State")]
    [SerializeField] private Image SelectedBackground;
    [SerializeField] private Button BuyButton;
    [SerializeField] private TextMeshProUGUI UseMark;
    [SerializeField] private TextMeshProUGUI UsedMark;
    [SerializeField] private Button SelectButton;

    private WeaponData CurrentData;

   


    private void Start()
    {
        
    }
    private void OnDestroy()
    {
        
    }
    public void SetData(WeaponData data)
    {
        CurrentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        WeaponId.SetText(CurrentData.ID.ToString());
        WeaponName.SetText(CurrentData.Name);
        Price.SetText(CurrentData.Price.ToString());
    }
}
