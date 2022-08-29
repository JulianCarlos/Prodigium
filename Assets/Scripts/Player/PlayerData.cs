using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public List<int> OwnedItemsID { get; private set; } = new List<int>();

    public List<ItemData> SelectedItems { get; private set; } = new List<ItemData>();

    [SerializeField] private ItemDatabase itemDatabase;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddItem(ItemData item)
    {
        OwnedItemsID.Add(item.ID);
        SaveManager.Instance.Save();
    }

    public void RemoveItem(ItemData item)
    {
        OwnedItemsID.Remove(item.ID);
        SaveManager.Instance.Save();
    }

    [ContextMenu("Reset Inventory Items")]
    public void ResetInventoryItems()
    {
        OwnedItemsID = new List<int>();
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

