using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvailableItemsPreview : MonoBehaviour
{
    [SerializeField] private AvailableItem availableItemPrefab;

    private void OnEnable()
    {
        Debug.Log("Count is: " + PlayerData.Instance.SelectedItems.Count);
        UpdateItemList();
    }

    private void UpdateItemList()
    {
        PlayerData.Instance.ResetSelectedItems();
        this.transform.DestroyChildren();

        foreach (var item in PlayerData.Instance.OwnedItems)
        {
            var instantiatedItem = Instantiate(availableItemPrefab, this.transform);
            instantiatedItem.SetValues(item);
        }
    }
}
