using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailableItem : MonoBehaviour
{
    [SerializeField] private string availableItemName;
    [SerializeField] private TextMeshProUGUI availableItemNameText;
    [SerializeField] private TextMeshProUGUI availableItemCountText;

    [SerializeField] private ItemData availableItem;

    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;

    private bool isSelected = false;
    private int index;

    public void SetValues(ItemData item)
    {
        availableItem = item;
        availableItemName = item.ItemName;
        availableItemNameText.text = item.ItemName;

        index = 0;
        UpdateButtonInteraction();
    }

    public void OnSelectItem()
    {
        if(!isSelected)
            PlayerData.Instance.SelectItem(availableItem);

        index++;
        availableItemCountText.text = index.ToString();

        isSelected = true;

        UpdateButtonInteraction();
    }
    public void OnDeSelectItem()
    {
        if(isSelected)
            PlayerData.Instance.DeSelectItem(availableItem);

        index--;
        availableItemCountText.text = index.ToString();

        isSelected = false;

        UpdateButtonInteraction();
    }
    
    private void UpdateButtonInteraction()
    {
        leftArrowButton.interactable = isSelected;
        rightArrowButton.interactable = !isSelected;
    }
}
