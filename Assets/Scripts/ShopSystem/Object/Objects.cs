using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : ScriptableObject
{
    public string ItemName => itemName;
    public GameObject PreviewItem => previewItem;
    public float Price => price;
    public bool IsBought => isBought;

    [SerializeField] protected string itemName;
    [SerializeField] protected GameObject previewItem;
    [SerializeField] protected float price;
    [SerializeField] protected bool isBought;
}
