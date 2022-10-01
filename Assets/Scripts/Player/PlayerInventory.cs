using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private Transform itemContainer;

    [SerializeField] private Item currentItem;
    [SerializeField] private ItemData currentItemData;

    [SerializeField] private List<Item> inventoryItems = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (var itemData in PlayerData.Instance.IngameItems)
        {
            var item = Instantiate(itemData.IngameItem, itemContainer);
            item.SetValues(itemData);
            item.gameObject.SetActive(false);
            inventoryItems.Add(item);
        }
    }

    public void SelectSegmentItem(ItemData selectedItem)
    {
        if((selectedItem == null)/* || (currentItem && currentItem.ItemData == selectedItem)*/)
        {
            currentItem?.gameObject.SetActive(false);
            currentItem = null;
            currentItemData = null;
            return;
        }
        else if (selectedItem)
        {
            currentItem?.gameObject.SetActive(false);
            currentItem = null;
            currentItemData = null;

            foreach (var item in inventoryItems)
            {
                if(item.ItemData.ID == selectedItem.ID)
                {
                    item.gameObject.SetActive(true);
                    currentItem = item;
                    currentItemData = item.ItemData;
                }
            }
        }
        Actions.OnItemChanged(currentItem);
    }
}
