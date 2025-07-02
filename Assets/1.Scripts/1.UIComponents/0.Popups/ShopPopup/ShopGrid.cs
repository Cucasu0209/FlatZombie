using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private ShopGridElement GridElementPrefab;
    [SerializeField] private RectTransform Content;
    private List<ShopGridElement> CurrentDisplayedElements;

    [Header("Category tittle")]
    [SerializeField] private Button MeleeCat;
    [SerializeField] private Button OneHGCat;
    [SerializeField] private Button TwoHGCat;

    public void UpdateWeaponList()
    {
        //Clear Cache
        if (CurrentDisplayedElements is null) CurrentDisplayedElements = new List<ShopGridElement>();
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
            ShopGridElement el = LeanPool.Spawn(GridElementPrefab, Content);
            el.transform.localScale = Vector3.one;
            CurrentDisplayedElements.Add(el);
            el.SetData(InventoryManager.Instance.ShopData.Weapons[i]);
        }
    }
}
