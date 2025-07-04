
using UnityEngine;
using UnityEngine.UI;

public class ShopCatButton : MonoBehaviour
{
    [SerializeField] private Button ChangeCatButton;
    [SerializeField] private WeaponType WeaponType;
    [SerializeField] private Image ActiveBG;

    private void Start()
    {
        ChangeCat();
        ChangeCatButton.onClick.AddListener(ChangeCat);
        InventoryManager.Instance.OnChangeCategory += OnChangeCategory;
    }
    private void OnDestroy()
    {
        InventoryManager.Instance.OnChangeCategory -= OnChangeCategory;
    }
    private void OnChangeCategory()
    {
        ActiveBG.gameObject.SetActive(WeaponType == InventoryManager.Instance.CurrentCategory);
    }
    private void ChangeCat()
    {
        InventoryManager.Instance.ChangeCategory(WeaponType);
    }
}
