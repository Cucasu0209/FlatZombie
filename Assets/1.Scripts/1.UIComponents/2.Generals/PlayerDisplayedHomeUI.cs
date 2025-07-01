using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplayedHomeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SkinIndex;
    private void Start()
    {
        UpdateSkin();
        PlayerData.Instance.OnCurrentSkinChange += UpdateSkin;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnCurrentSkinChange -= UpdateSkin;

    }

    private void UpdateSkin()
    {
        SkinIndex.SetText($"Skin {PlayerData.Instance.CurrentSkin}");
    }
}
