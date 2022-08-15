using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static List<Item> OwnedItems = new List<Item>();
    public static List<Item> ItemsInUse = new List<Item>();

    public static void AddItemsIngame(List<Item> targetInventory)
    {
        foreach (Item item in ItemsInUse)
        {
            targetInventory.Add(item);
            ItemsInUse.Remove(item);
        }
    }

    public static void AddItem(Item item)
    {
        OwnedItems.Add(item);
    }

    public static void RemoveItem(Item item)
    {
        OwnedItems.Remove(item);
    }
}

