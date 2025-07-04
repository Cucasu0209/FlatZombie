using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatTableCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DataDisplay;

    public void SetData(string data)
    {
        DataDisplay.text = data;
    }
}
