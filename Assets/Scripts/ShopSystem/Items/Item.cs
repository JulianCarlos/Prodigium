using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName => itemName;
    public string ItemTooltip => itemTooltip;
    public float Price => price;
    public bool IsBought { get => isBought; set => isBought = value; }
    public GameObject PreviewItem => previewItem;

    [SerializeField] protected string itemName;
    [SerializeField] protected string itemTooltip;
    [SerializeField] protected float price;
    [SerializeField] protected bool isBought;
    [SerializeField] protected GameObject previewItem;
}
