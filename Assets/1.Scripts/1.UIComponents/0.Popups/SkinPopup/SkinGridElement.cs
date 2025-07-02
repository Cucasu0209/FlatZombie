using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinGridElement : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private TextMeshProUGUI SkinId;
    [SerializeField] private TextMeshProUGUI SkinName;
    [SerializeField] private TextMeshProUGUI Price;

    [Header("State")]
    [SerializeField] private Image SelectedBackground;
    [SerializeField] private Button BuyButton;
    [SerializeField] private TextMeshProUGUI UseMark;
    [SerializeField] private TextMeshProUGUI UsedMark;
    [SerializeField] private Button SelectButton;

    private SkinData CurrentData;


    private void Start()
    {
        BuyButton.onClick.AddListener(OnBuyButtonClick);
        SelectButton.onClick.AddListener(OnSelectButtonClick);
        InventoryManager.Instance.OnSelectSkin += OnOneSkinSelected;
        PlayerData.Instance.OnCurrentSkinUsedChange += OnOneSkinUsed;
    }
    private void OnDestroy()
    {
        InventoryManager.Instance.OnSelectSkin -= OnOneSkinSelected;
        PlayerData.Instance.OnCurrentSkinUsedChange -= OnOneSkinUsed;
    }
    public void SetData(SkinData data)
    {
        CurrentData = data;
        UpdateState();
    }
    private void UpdateState()
    {
        SkinId.SetText(CurrentData.ID.ToString());
        SkinName.SetText(CurrentData.Name);
        Price.SetText(CurrentData.Price.ToString());


        bool haveSkin = PlayerData.Instance.HaveSkin(CurrentData.ID);
        BuyButton.gameObject.SetActive(haveSkin == false);
        UseMark.gameObject.SetActive(haveSkin && CurrentData.ID != PlayerData.Instance.CurrentSkinIdUsed);
        UsedMark.gameObject.SetActive(haveSkin && CurrentData.ID == PlayerData.Instance.CurrentSkinIdUsed);
        SelectedBackground.gameObject.SetActive(CurrentData.ID == InventoryManager.Instance.CurrentSkinIdSelected);
    }
    private void OnSelectButtonClick()
    {
        InventoryManager.Instance.SelectSkin(CurrentData.ID);
    }
    private void OnBuyButtonClick()
    {
        InventoryManager.Instance.RequestBuySkin(CurrentData.ID);

    }
    private void OnOneSkinSelected()
    {
        UpdateState();
    }
    private void OnOneSkinUsed()
    {
        UpdateState();
    }
}
