using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject tooltipWindow;
    [SerializeField] private TextMeshProUGUI tooltipText;

    ShopSystem shopSystem;

    private void Start()
    {
        shopSystem = FindObjectOfType<ShopSystem>();
    }

    public void ShowTooltip()
    {
        tooltipWindow.SetActive(true);
        tooltipText.text = shopSystem.CurrentSelectedObject.ItemTooltip;
    }

    public void HideTooltip()
    {
        tooltipWindow.SetActive(false);
    }
}
