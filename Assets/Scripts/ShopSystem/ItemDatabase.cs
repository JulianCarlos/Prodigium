using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Item[] items;

    [ContextMenu("Get all Items")]
    public void GetAllItems()
    {
        items = AssetFinder.GetAllInstances<Item>();
    }

    [ContextMenu("Apply IDs")]
    public void ApplyID()
    {
        int id = 0;

        foreach (var item in items)
        {
            item.ID = id;
            id++;
        }
    }
}
