using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CheatTableColumn : MonoBehaviour
{
    [SerializeField] private CheatTableCell Tittle;
    [SerializeField] private CheatTableCell CellDataPrefab;
    [SerializeField] private RectTransform Content;
    private List<CheatTableCell> CurrentCells;
    public void SetData(List<string> data)
    {
        if (CurrentCells is null) CurrentCells = new List<CheatTableCell>();
        else
        {
            for (int i = 0; i < CurrentCells.Count; i++)
                LeanPool.Despawn(CurrentCells[i].gameObject);
            CurrentCells.Clear();
        }

        Tittle.SetData(data[0]);
        for (int i = 1; i < data.Count; i++)
        {
            CheatTableCell cell = LeanPool.Spawn(CellDataPrefab, Content);
            cell.transform.localScale = Vector3.one;
            cell.transform.SetAsLastSibling();
            cell.SetData(data[i]);
            CurrentCells.Add(cell);
        }
    }
}
