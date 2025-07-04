using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class CheatTableData : MonoBehaviour
{
    [SerializeField] private CheatTableColumn ColumnPrefab;
    [SerializeField] private RectTransform Content;
    private List<CheatTableColumn> CurrentColumns;

    public void SetData(List<List<string>> data)
    {
        StopAllCoroutines();
        StartCoroutine(ShowData(data));
    }
    private IEnumerator ShowData(List<List<string>> data)
    {
        if (CurrentColumns is null) CurrentColumns = new List<CheatTableColumn>();
        else
        {
            for (int i = 0; i < CurrentColumns.Count; i++)
                LeanPool.Despawn(CurrentColumns[i].gameObject);
            CurrentColumns.Clear();
        }

        for (int i = 0; i < data[0].Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            CheatTableColumn column = LeanPool.Spawn(ColumnPrefab, Content);

            List<string> columndata = new List<string>();

            for (int j = 0; j < data.Count; j++)
            {
                columndata.Add(data[j][i]);
            }
            column.transform.localScale = Vector3.one;
            column.transform.SetAsLastSibling();
            column.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            column.SetData(columndata);
            CurrentColumns.Add(column);
            Content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;

        }
        yield return new WaitForSeconds(0.1f);
        GameObject D = Instantiate(new GameObject("a"), Content);
        Destroy(D, 0.1f);
    }
}
