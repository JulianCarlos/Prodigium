using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private Transform itemContainer;

    [SerializeField] private Item currentItem;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InstantiateItem(ItemData selectedItem)
    {
        if(currentItem != null && currentItem == selectedItem)
        {
            return;
        }

        itemContainer.DestroyChildren();

        if (selectedItem == null)
            return;

        currentItem = selectedItem.PreviewItem;
        var item = Instantiate(currentItem, itemContainer);
    }
}
