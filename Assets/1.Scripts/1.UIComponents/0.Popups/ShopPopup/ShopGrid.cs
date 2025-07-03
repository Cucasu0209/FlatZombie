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

    private void Start()
    {
        InventoryManager.Instance.OnChangeCategory += UpdateWeaponList;
    }
    private void OnDestroy()
    {
        InventoryManager.Instance.OnChangeCategory -= UpdateWeaponList;
    }
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
        for (int i = 0; i < InventoryManager.Instance.WeaponDatas_Type[InventoryManager.Instance.CurrentCategory].Count; i++)
        {
            ShopGridElement el = LeanPool.Spawn(GridElementPrefab, Content);
            el.transform.localScale = Vector3.one;
            CurrentDisplayedElements.Add(el);
            el.SetData(InventoryManager.Instance.WeaponDatas_Type[InventoryManager.Instance.CurrentCategory][i]);
        }
    }
}
