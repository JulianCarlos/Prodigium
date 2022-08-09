using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltipWindow;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private bool canUse;

    private void Awake()
    {
        Actions.OnSelectedItemChanged += UpdateTooltipText;
    }

    public void ShowTooltip()
    {
        if (!canUse)
            return;

        tooltipWindow.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipWindow.SetActive(false);
    }

    public void UpdateTooltipText(Item item)
    {
        tooltipText.text = item.ItemTooltip;

        canUse = item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
