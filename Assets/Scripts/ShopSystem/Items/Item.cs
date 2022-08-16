using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName => itemName;
    public string ItemTooltip => itemTooltip;
    public float Price => price;
    public GameObject PreviewItem => previewItem;

    [SerializeField] protected string itemName;
    [SerializeField, TextArea(10, 100)] protected string itemTooltip;
    [SerializeField] protected float price;
    [SerializeField] protected GameObject previewItem;
}
