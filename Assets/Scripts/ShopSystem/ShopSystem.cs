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

    [Header("Preview Buttons")]
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button useButton;

    [Header("Current Object")]
    [SerializeField] private int currentItemIndex = 0;
    [SerializeField] private Objects currentSelectedObject;
    [SerializeField] private List<Objects> currentCategoryItems;

    [Header("Items")]
    [SerializeField] private Transform previewItemContainer;
    [SerializeField] private Transform shopCategoryContainer;

    [Header("List of Categories")]
    [SerializeField] private List<ShopCategory> shopCategories;

    [Header("Prefabs")]
    [SerializeField] private GameObject shopCategoryPrefab;

    private void Awake()
    {
        currentCategoryItems = shopCategories[0].ShopCategoryObjects;
        currentSelectedObject = currentCategoryItems[currentItemIndex];

        SetPreviewValues(0);
        Cursor.lockState = CursorLockMode.None;
    }

    private void SetPreviewValues(int input)
    {
        transform.DOKill();
        if(currentSelectedObject != null)
        {
            var obj = Instantiate(currentSelectedObject.PreviewItem, previewItemContainer);
            //obj.transform.localPosition = Vector3.zero;
            obj.transform.localPosition = Vector3.zero + (input > 0 ? 1 : -1) * (transform.right * 2);
            obj.transform.DOLocalMove( Vector3.zero, 0.3f);

            previewItemTitleText.text = currentSelectedObject.ItemName;
            previewItemPriceText.text = "Buy: " + currentSelectedObject.Price.ToString() + "$";
            unlockedCountText.text = $"{currentCategoryItems.IndexOf(currentSelectedObject) + 1} / {currentCategoryItems.Count}";
        }
        else
        {
            previewItemTitleText.text = string.Empty;
            previewItemPriceText.text = string.Empty;
            unlockedCountText.text = string.Empty;
        }

        buyButton.gameObject.SetActive(currentCategoryItems.Count != 0 && !currentSelectedObject.IsBought);
        useButton.gameObject.SetActive(currentCategoryItems.Count != 0 && currentSelectedObject.IsBought);

        leftArrowButton.interactable = currentItemIndex != 0;
        rightArrowButton.interactable = currentItemIndex != currentCategoryItems.Count - 1 && currentCategoryItems.Count != 0;
    }

    public void ChangeItem(int input)
    {
        currentSelectedObject = null;
        previewItemContainer.DestroyChildren();

        currentItemIndex += input;

        if(currentCategoryItems.Count > 0)
            currentSelectedObject = currentCategoryItems[currentItemIndex];

        SetPreviewValues(input);
    }

    public void ChangeCategory(ShopCategory category)
    {
        if (currentCategoryItems == category.ShopCategoryObjects)
            return;

        currentCategoryItems = category.ShopCategoryObjects;
        currentItemIndex = 0;

        unlockedCountText.text = $"0 / {currentCategoryItems.Count}";
        
        ChangeItem(0);
    }
}
