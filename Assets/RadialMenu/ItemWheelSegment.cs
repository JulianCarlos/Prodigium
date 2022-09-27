using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemWheelSegment : MonoBehaviour
{
    public Image Icon => icon;
    public Image CakePiece => cakePiece;
    public Image SitePrefab => sitePrefab;

    public ItemData CurrentSelectedItem { get; private set; }
    public List<ItemData> CategoryItems { get; private set; }


    [SerializeField] private Image icon;
    [SerializeField] private Image cakePiece;

    [SerializeField] private Image sitePrefab;
    [SerializeField] private Transform sideContainer;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color selectedColor;

    [SerializeField] private ItemCategoryType categoryType;

    [Space]
    [SerializeField] private int currentSelectedItemIndex;

    [SerializeField] List<Image> siteItems;

    private Vector3 originalPos;

    private void Awake()
    {
        CategoryItems = PlayerData.Instance.SortSelectedItemsIntoCategories(categoryType);

        SetIcon();
    }

    private void Start()
    {
        originalPos = transform.localPosition;

        currentSelectedItemIndex = 0;

        if (CategoryItems.Count > 0)
            CurrentSelectedItem = CategoryItems[currentSelectedItemIndex];
    }

    private void SetIcon()
    {
        for (int i = 0; i < CategoryItems.Count; i++)
        {
            var siteElement = Instantiate(sitePrefab, sideContainer);
            siteItems.Add(siteElement);
        }

        if (siteItems.Count > 0)
            siteItems[0].color = selectedColor;

        if (CategoryItems.Count > 0)
        {
            icon.enabled = true;
            icon.sprite = CategoryItems[0].ItemIcon;
            siteItems[0].color = selectedColor;
        }
        else
        {
            icon.enabled = false;
            icon = null;
        }
    }

    public void ChangeSegmentColor(Color targetColor)
    {
        cakePiece.color = targetColor;
    }

    private void UpdateIcon()
    {
        icon.sprite = CategoryItems[currentSelectedItemIndex].ItemIcon;
    }

    public void ScrollPrevious()
    {
        if (CategoryItems.Count > 0 && currentSelectedItemIndex != 0)
        {
            siteItems[currentSelectedItemIndex].color = baseColor;
            currentSelectedItemIndex--;
            CurrentSelectedItem = CategoryItems[currentSelectedItemIndex];
            siteItems[currentSelectedItemIndex].color = selectedColor;
            UpdateIcon();
        }
    }

    public void ScrollNext()
    {
        if (CategoryItems.Count > 0 && currentSelectedItemIndex != CategoryItems.Count - 1)
        {
            siteItems[currentSelectedItemIndex].color = baseColor;
            currentSelectedItemIndex++;
            CurrentSelectedItem = CategoryItems[currentSelectedItemIndex];
            siteItems[currentSelectedItemIndex].color = selectedColor;
            UpdateIcon();
        }
    }
}
