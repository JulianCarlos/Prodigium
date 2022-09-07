using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public int ID {get => id; set=> id = value; }   
    public string ItemName => itemName;
    public string ItemTooltip => itemTooltip;
    public float Price => price;
    public Item PreviewItem => previewItem;
    public ItemCategoryType ItemCategoryType => itemCategoryType;
    public Sprite ItemIcon => itemIcon;

    [SerializeField] protected int id;
    [SerializeField] protected string itemName;
    [SerializeField, TextArea(10, 100)] protected string itemTooltip;
    [SerializeField] protected float price;
    [SerializeField] protected Item previewItem;
    [SerializeField] protected ItemCategoryType itemCategoryType;
    [SerializeField] protected Sprite itemIcon;
}
