using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static List<Item> OwnedItems = new List<Item>();

    public static List<Weapons> OwnedWeapons = new List<Weapons>();
    public static List<Vehicles> OwnedVehicles = new List<Vehicles>();

    public static void AddItem(Item item)
    {
        if (item.GetType() == typeof(Weapons))
        {
            OwnedWeapons.Add((Weapons)item);
        }
        else if (item.GetType() == typeof(Vehicles))
        {
            OwnedVehicles.Add((Vehicles)item);
        }
    }

    public static void RemoveItem(Item item)
    {
        OwnedItems.Remove(item);
    }
}
