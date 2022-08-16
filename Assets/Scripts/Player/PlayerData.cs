using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public List<int> OwnedItemsID = new List<int>();
    public List<Item> OwnedItems = new List<Item>();

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


    //Save Methods
    public object CaptureState()
    {
        return new SaveData
        {
            OwnedItemsID = OwnedItemsID
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        OwnedItemsID = saveData.OwnedItemsID;
    }

    [Serializable]
    public struct SaveData
    {
        public List<int> OwnedItemsID;
    }
}

