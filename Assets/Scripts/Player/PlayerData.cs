using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public List<int> OwnedItemsID { get; private set; } = new List<int>();

    public List<ItemData> OwnedItems { get; private set; } = new List<ItemData>();
    public List<ItemData> SelectedItems { get; private set; } = new List<ItemData>();
    public List<ItemData> IngameItems { get; private set; } = new List<ItemData>();


    [SerializeField] private ItemDatabase itemDatabase;

    protected override void Awake()
    {
        base.Awake();

        GetItemsBySavedIDs();
    }

    public void TransferItemsToIngameInventory()
    {
        IngameItems = SelectedItems;

        SelectedItems = new List<ItemData>();

        var ingameIDs = IngameItems.Select(i => i.ID);

        OwnedItemsID = OwnedItemsID.Where(i => !ingameIDs.Contains(i)).ToList();

        SaveManager.Instance.Save();
    }

    public void ResetSelectedItems()
    {
        SelectedItems.Clear();
    }

    public void SelectItem(ItemData item)
    {
        if (itemDatabase == null)
            return;

        SelectedItems.Add(itemDatabase.GetItemByID(item.ID));
    }

    public void DeSelectItem(ItemData item)
    {
        if (itemDatabase == null)
            return;

        SelectedItems.Remove(itemDatabase.GetItemByID(item.ID));
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

        for (int i = 0; i < IngameItems.Count; i++)
        {
            if(IngameItems[i].ItemCategoryType == type)
            {
                targetList.Add(IngameItems[i]);
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

