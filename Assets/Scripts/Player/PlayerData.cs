using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public List<int> OwnedItemsID { get; private set; } = new List<int>();

    public List<Item> SelectedItems { get; private set; } = new List<Item>();

    [SerializeField] private ItemDatabase itemDatabase;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddItem(Item item)
    {
        OwnedItemsID.Add(item.ID);
        SaveManager.Instance.Save();
    }

    public void RemoveItem(Item item)
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

