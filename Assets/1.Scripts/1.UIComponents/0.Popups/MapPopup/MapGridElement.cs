using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGridElement : MonoBehaviour
{
    [SerializeField] private Button SelectBtn;
    [SerializeField] private TextMeshProUGUI MapDescription;
    [SerializeField] private Image MapBG;
    private LevelData currentData;

    private void Start()
    {

    }
    private void OnDestroy()
    {

    }
    public void SetData(LevelData data)
    {
        currentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        MapDescription.SetText($"Map {currentData.Index}: {currentData.Description}");
    }
}
