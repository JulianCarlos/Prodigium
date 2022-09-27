using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private ItemData currentItem;

    [SerializeField] private Transform itemContainer;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InstantiateItem(ItemData selectedItem)
    {
        if(currentItem?.ID == selectedItem.ID)
            return;

        itemContainer.DestroyChildren();

        Actions.OnItemChanged(selectedItem);

        if (selectedItem == null)
            return;

        currentItem = selectedItem;
        var item = Instantiate(currentItem.IngameItem, itemContainer);
    }
}
