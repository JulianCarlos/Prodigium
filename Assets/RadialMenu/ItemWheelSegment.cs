using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemWheelSegment : MonoBehaviour
{
    public Image Icon;
    public Image CakePiece;

    [SerializeField] private ItemCategoryType categoryType;

    [Space]
    [SerializeField] private int currentSelectedItemIndex;

    public ItemData CurrentSelectedItem { get; private set; }
    public List<ItemData> CategoryItems;

    private Vector3 originalPos;

    private void Awake()
    {
        PlayerData.Instance.SortSelectedItemsIntoCategories(categoryType, CategoryItems);

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
        if (CategoryItems.Count > 0)
        {
            Icon.enabled = true;
            Icon.sprite = CategoryItems[0].ItemIcon;
        }
        else
        {
            Icon.enabled = false;
            Icon = null;
        }
    }

    public void ScrollPrevious()
    {
        if (CategoryItems.Count > 0 && currentSelectedItemIndex != 0)
        {
            currentSelectedItemIndex--;
            CurrentSelectedItem = CategoryItems[currentSelectedItemIndex];
        }
    }

    public void ScrollNext()
    {
        if (CategoryItems.Count > 0 && currentSelectedItemIndex != CategoryItems.Count - 1)
        {
            currentSelectedItemIndex++;
            CurrentSelectedItem = CategoryItems[currentSelectedItemIndex];
        }
    }
}
