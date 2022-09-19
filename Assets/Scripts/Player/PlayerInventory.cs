using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private Item currentItem;

    [SerializeField] private Transform itemContainer;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InstantiateItem(ItemData selectedItem)
    {
        if(currentItem == selectedItem)
            return;

        itemContainer.DestroyChildren();

        if (selectedItem == null)
            return;

        currentItem = selectedItem.PreviewItem;
        var item = Instantiate(currentItem, itemContainer);
    }
}
