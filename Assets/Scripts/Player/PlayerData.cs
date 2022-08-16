using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>, ISaveable<object>
{
    public static List<Item> OwnedItems = new List<Item>();
    public static List<Item> ItemsInUse = new List<Item>();

    [SerializeField] private float testFloat;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddItemsIngame(List<Item> targetInventory)
    {
        foreach (Item item in ItemsInUse)
        {
            targetInventory.Add(item);
            ItemsInUse.Remove(item);
        }
    }

    public void AddItem(Item item)
    {
        OwnedItems.Add(item);
        Debug.Log(OwnedItems.Count);
        SaveManager.Instance.Save();
    }

    public void RemoveItem(Item item)
    {
        OwnedItems.Remove(item);
        SaveManager.Instance.Save();
    }


    //Save Methods
    public object CaptureState()
    {
        return new SaveData
        {
            Testfloat = testFloat,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        testFloat = saveData.Testfloat;
    }

    [Serializable]
    public struct SaveData
    {
        public float Testfloat;
    }
}

