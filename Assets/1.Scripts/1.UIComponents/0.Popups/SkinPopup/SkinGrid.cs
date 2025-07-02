using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class SkinGrid : MonoBehaviour
{
    [SerializeField] private SkinGridElement GridElementPrefab;
    [SerializeField] private RectTransform Content;

    private List<SkinGridElement> CurrentDisplayedElements;
    public void UpdateSkinList()
    {
        //Clear Cache
        if (CurrentDisplayedElements is null) CurrentDisplayedElements = new List<SkinGridElement>();
        else
        {
            for (int i = 0; i < CurrentDisplayedElements.Count; i++)
            {
                //despawn
                LeanPool.Despawn(CurrentDisplayedElements[i].gameObject);
            }
            CurrentDisplayedElements.Clear();
        }


        //Spawn New
        for (int i = 0; i < InventoryManager.Instance.ShopData.Skins.Count; i++)
        {
            SkinGridElement el = LeanPool.Spawn(GridElementPrefab, Content);
            el.transform.localScale = Vector3.one;
            CurrentDisplayedElements.Add(el);
            el.SetData(InventoryManager.Instance.ShopData.Skins[i]);
        }
    }
}
