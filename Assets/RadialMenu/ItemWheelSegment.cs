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
    [SerializeField] private ItemData currentSelectedItem;
    [SerializeField] private int currentSelectedItemIndex;

    [Space]
    [SerializeField] private List<ItemData> categoryItems;

    private Vector3 originalPos;

    private void Awake()
    {
        PlayerData.Instance.SortSelectedItemsIntoCategories(categoryType, categoryItems);

        SetIcon();
    }

    private void Start()
    {
        originalPos = transform.localPosition;

        currentSelectedItemIndex = 0;

        if (categoryItems.Count > 0)
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
    }

    private void SetIcon()
    {
        if (categoryItems.Count > 0)
        {
            Icon.enabled = true;
            Icon = categoryItems[0].ItemIcon;
        }
        else
        {
            Icon.enabled = false;
            Icon = null;
        }
    }

    public void ScrollPrevious()
    {
        if (categoryItems.Count > 0 && currentSelectedItemIndex != 0)
        {
            currentSelectedItemIndex--;
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
        }
    }

    public void ScrollNext()
    {
        if (categoryItems.Count > 0 && currentSelectedItemIndex != categoryItems.Count - 1)
        {
            currentSelectedItemIndex++;
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
        }
    }
}
