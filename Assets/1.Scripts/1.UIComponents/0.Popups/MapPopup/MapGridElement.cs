using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGridElement : MonoBehaviour
{
    [SerializeField] private Button SelectBtn;
    [SerializeField] private TextMeshProUGUI MapDescription;
    [SerializeField] private Image SelectBG;
    private LevelData currentData;

    private void Start()
    {
        SelectBtn.onClick.AddListener(OnElementClick);
        LevelManager.Instance.OnCurrentLevelChange += UpdateState;
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnCurrentLevelChange -= UpdateState;

    }
    public void SetData(LevelData data)
    {
        currentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        MapDescription.SetText($"Map {currentData.Index}: {currentData.Description}");
        SelectBG.gameObject.SetActive(currentData.Index == LevelManager.Instance.CurrentLevelIndex);
    }
    private void OnElementClick()
    {
        LevelManager.Instance.ChangeLevel(currentData.Index);
    }
}
