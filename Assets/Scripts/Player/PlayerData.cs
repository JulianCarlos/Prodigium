using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public List<int> OwnedItemsID { get; private set; } = new List<int>();
    public List<ItemData> OwnedItems { get; private set; } = new List<ItemData>();

    public List<ItemData> SelectedItems { get; private set; } = new List<ItemData>();

    [SerializeField] private ItemDatabase itemDatabase;

    protected override void Awake()
    {
        base.Awake();

        GetItemsBySavedIDs();
    }

    public void SelectItem(ItemData item)
    {
        if (itemDatabase == null)
            return;

        SelectedItems.Add(itemDatabase.GetItemByID(item.ID));
        OwnedItems.Remove(item);
        OwnedItemsID.Remove(item.ID);
        Debug.Log("Selected Items are; " + SelectedItems.Count);
        Debug.Log("OwnedItems are; " + OwnedItems.Count);
    }

    public void DeSelectItem(ItemData item)
    {
        if (itemDatabase == null)
            return;

        SelectedItems.Remove(itemDatabase.GetItemByID(item.ID));
        OwnedItems.Add(item);
        OwnedItemsID.Add(item.ID);
        Debug.Log("Selected Items are; " + SelectedItems.Count);
        Debug.Log("OwnedItems are; " + OwnedItems.Count);
    }

    private void GetItemsBySavedIDs()
    {
        foreach (var itemID in OwnedItemsID)
        {
            OwnedItems.Add(itemDatabase.GetItemByID(itemID));
        }
    }

    public List<ItemData> SortSelectedItemsIntoCategories(ItemCategoryType type)
    {
        List<ItemData> targetList = new List<ItemData>();

        for (int i = 0; i < SelectedItems.Count; i++)
        {
            if(SelectedItems[i].ItemCategoryType == type)
            {
                targetList.Add(SelectedItems[i]);
                OwnedItemsID.Remove(SelectedItems[i].ID);
            }
        }
        return targetList;
    }

    public void AddItem(ItemData item)
    {
        OwnedItemsID.Add(item.ID);
        OwnedItems.Add(item);
        SaveManager.Instance.Save();
    }

    public void RemoveItem(ItemData item)
    {
        OwnedItemsID.Remove(item.ID);
        OwnedItems.Remove(item);
        SaveManager.Instance.Save();
    }

    [ContextMenu("Reset Inventory Items")]
    public void ResetInventoryItems()
    {
        OwnedItemsID.Clear();
        OwnedItems.Clear();
    }

    //Save Methods
    public object CaptureState()
    {
        return new SaveData
        {
            OwnedItemsID = OwnedItemsID,
            CurrentMoney = MoneySystem.Currency
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        OwnedItemsID = saveData.OwnedItemsID;
        MoneySystem.Currency = saveData.CurrentMoney;
    }

    [Serializable]
    internal struct SaveData
    {
        internal List<int> OwnedItemsID;
        internal float CurrentMoney;
    }
}

