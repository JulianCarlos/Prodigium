using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ShopSystem : MonoBehaviour
{
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
    public Item CurrentSelectedObject;
    [SerializeField] private ShopCategory currentCategory;

    [Header("Container")]
    [SerializeField] private Transform previewItemContainer;
    [SerializeField] private Transform shopCategoryContainer;

    [Header("List of Categories")]
    [SerializeField] private List<ShopCategory> shopCategories;

    private void Awake()
    {
        currentCategory = shopCategories[0];
        CurrentSelectedObject = currentCategory.Items[0];

        ChangeItem(0);
        Cursor.lockState = CursorLockMode.None;
    }

    private void SetPreviewValues(int input)
    {
        if(CurrentSelectedObject != null)
        {
            previewItemTitleText.text = CurrentSelectedObject.ItemName;
            previewItemPriceText.text = "Buy: " + CurrentSelectedObject.Price.ToString() + "$";
            unlockedCountText.text = $"{currentCategory.Items.IndexOf(CurrentSelectedObject) + 1} / {currentCategory.Items.Count}";
        }
        else
        {
            previewItemTitleText.text = string.Empty;
            previewItemPriceText.text = string.Empty;
            unlockedCountText.text = string.Empty;
        }

        buyButton.gameObject.SetActive(currentCategory.Items.Count != 0 && !CurrentSelectedObject.IsBought);
        useButton.gameObject.SetActive(currentCategory.Items.Count != 0 && CurrentSelectedObject.IsBought);

        leftArrowButton.interactable = currentItemIndex != 0;
        rightArrowButton.interactable = currentItemIndex != currentCategory.Items.Count - 1 && currentCategory.Items.Count != 0;

        moneyCountText.text = "Balance: " + MoneySystem.Currency.ToString("0.0") + "$";
    }

    private void PreviewObjectInstantiation(int input)
    {
        transform.DOKill();
        
        if (CurrentSelectedObject == null) 
            return;
        
        var obj = Instantiate(CurrentSelectedObject.PreviewItem, previewItemContainer);
        obj.transform.localPosition = Vector3.zero + (input > 0 ? 1 : -1) * (transform.right * 2);
        obj.transform.DOLocalMove(Vector3.zero, 0.3f);
    }

    public void ChangeItem(int input)
    {
        CurrentSelectedObject = null;
        previewItemContainer.DestroyChildren();

        currentItemIndex += input;

        if(currentCategory.Items.Count > 0)
            CurrentSelectedObject = currentCategory.Items[currentItemIndex];

        PreviewObjectInstantiation(input);
        SetPreviewValues(input);
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

    public void BuyObject()
    {
        float price = CurrentSelectedObject.Price;

        if (MoneySystem.MoneyCheck(price))
        {
            MoneySystem.RemoveMoney(price);
            CurrentSelectedObject.IsBought = true;

            PlayerData.AddItem(CurrentSelectedObject);

            SetPreviewValues(0);
        }
    }
}
