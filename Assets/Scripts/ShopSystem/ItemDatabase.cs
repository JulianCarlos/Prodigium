using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Item/Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items;

    public ItemData GetItemByID(int ID)
    {
        return items.FirstOrDefault(i => i.ID == ID);
    }
    
    [ContextMenu("Get all Items")]
    public void GetAllItems()
    {
        items = AssetFinder.GetAllInstances<ItemData>().ToList();
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
