using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ShopSystem : MonoBehaviour
{
    public Item CurrentSelectedItem { get; private set; }
    public GameObject CurrentSelectedItemGameobject { get; private set; }

    [Header("UI Titles")]
    [SerializeField] private TextMeshProUGUI previewItemTitleText;
    [SerializeField] private TextMeshProUGUI previewItemPriceText;
    [SerializeField] private TextMeshProUGUI unlockedCountText;
    [SerializeField] private TextMeshProUGUI moneyCountText;

    [Header("UI Buttons")]
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button useButton;
    [Space]
    [SerializeField] private Button shopCategoryPrefab;

    [Header("Current Object")]
    [SerializeField] private int currentItemIndex = 0;
    [SerializeField] private ShopCategory currentCategory;

    [Header("Container")]
    [SerializeField] private Transform previewItemContainer;
    [SerializeField] private Transform shopCategoryContainer;

    [Header("List of Categories")]
    [SerializeField] private List<ShopCategory> shopCategories;

    private Vector3 originalPreviewContainerPosition;

    private void Awake()
    {
        originalPreviewContainerPosition = previewItemContainer.localPosition;

        currentCategory = shopCategories[0];
        CurrentSelectedItem = currentCategory.Items[0];

        Actions.OnMoneyChanged += SetCurrencyTextAmount;

        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        ChangeItem(0);
        SetCurrencyTextAmount(MoneySystem.Currency);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            MoneySystem.AddMoney(100000f);
        }
    }

    private void SetPreviewValues(int input)
    {
        if(CurrentSelectedItem != null)
        {
            previewItemTitleText.text = CurrentSelectedItem.ItemName;
            previewItemPriceText.text = "Buy: " + CurrentSelectedItem.Price.ToString() + "$";
            unlockedCountText.text = $"{currentCategory.Items.IndexOf(CurrentSelectedItem) + 1} / {currentCategory.Items.Count}";
        }
        else
        {
            previewItemTitleText.text = string.Empty;
            previewItemPriceText.text = string.Empty;
            unlockedCountText.text = string.Empty;
        }

        buyButton.gameObject.SetActive(CurrentSelectedItem && currentCategory.Items.Count != 0 && !PlayerData.Instance.OwnedItemsID.Contains(CurrentSelectedItem.ID));
        useButton.gameObject.SetActive(CurrentSelectedItem && currentCategory.Items.Count != 0 && PlayerData.Instance.OwnedItemsID.Contains(CurrentSelectedItem.ID));

        leftArrowButton.interactable = currentItemIndex != 0;
        rightArrowButton.interactable = currentItemIndex != currentCategory.Items.Count - 1 && currentCategory.Items.Count != 0;
    }

    private void PreviewObjectInstantiation(int input)
    {
        if (CurrentSelectedItem == null) 
            return;

        transform.DOKill();
        
        CurrentSelectedItemGameobject = Instantiate(CurrentSelectedItem.PreviewItem, previewItemContainer);
        CurrentSelectedItemGameobject.transform.localPosition = Vector3.zero + (input > 0 ? 1 : -1) * (transform.right * 2);
        CurrentSelectedItemGameobject.transform.DOLocalMove(Vector3.zero, 0.3f);
    }

    public void ChangeItem(int input)
    {
        previewItemContainer.transform.localPosition = originalPreviewContainerPosition;

        CurrentSelectedItem = null;
        previewItemContainer.DestroyChildren();

        currentItemIndex += input;

        if(currentCategory.Items.Count > 0)
        {
            CurrentSelectedItem = currentCategory.Items[currentItemIndex];
        }

        PreviewObjectInstantiation(input);
        SetPreviewValues(input);
        Actions.OnSelectedItemChanged(CurrentSelectedItem);
    }

    public void ChangeCategory(ShopCategory category)
    {
        if (currentCategory == category)
            return;

        currentCategory = category;
        currentItemIndex = 0;

        unlockedCountText.text = $"0 / {currentCategory.Items.Count}";
        
        ChangeItem(0);
    }

    public void SetCurrencyTextAmount(float amount)
    {
        moneyCountText.text = "Balance: " + MoneySystem.Currency.ToString() + "$";
    }

    public void BuyObject()
    {
        float price = CurrentSelectedItem.Price;

        if (MoneySystem.MoneyCheck(price))
        {
            MoneySystem.RemoveMoney(price);
            PlayerData.Instance.AddItem(CurrentSelectedItem);

            SetPreviewValues(0);
        }
    }
}
