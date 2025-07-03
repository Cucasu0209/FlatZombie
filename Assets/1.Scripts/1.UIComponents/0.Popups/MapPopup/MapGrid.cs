using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private RectTransform Content;
    [SerializeField] private MapGridElement ElementPrefab;

    private List<MapGridElement> CurrentElements;
    public void UpdateGrid()
    {
        //Clear Cache
        if (CurrentElements is null) CurrentElements = new List<MapGridElement>();
        else
        {
            for (int i = 0; i < CurrentElements.Count; i++)
            {
                LeanPool.Despawn(CurrentElements[i].gameObject);
            }
            CurrentElements.Clear();
        }

        //Spawn New -> get data from list -> render
        List<LevelData> levels = LevelManager.Instance.CurrentMapData.Levels;

        for (int i = 0; i < levels.Count; i++)
        {
            MapGridElement el = LeanPool.Spawn(ElementPrefab, Content);
            el.transform.localScale = Vector3.one;
            CurrentElements.Add(el);
            el.SetData(levels[i]);
        }

    }
}
