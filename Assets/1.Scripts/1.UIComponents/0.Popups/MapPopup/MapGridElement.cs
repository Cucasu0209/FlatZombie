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
    private LevelDetailData currentData;

    private void Start()
    {
        SelectBtn.onClick.AddListener(OnElementClick);
        LevelManager.Instance.OnCurrentLevelChange += UpdateState;
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnCurrentLevelChange -= UpdateState;

    }
    public void SetData(LevelDetailData data)
    {
        currentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        MapDescription.SetText($"Map {currentData.Level}: {currentData.Map.ToString()}");
        SelectBG.gameObject.SetActive(currentData.Level == LevelManager.Instance.CurrentLevelIndex);
    }
    private void OnElementClick()
    {
        LevelManager.Instance.ChangeLevel(currentData.Level);
    }
}
