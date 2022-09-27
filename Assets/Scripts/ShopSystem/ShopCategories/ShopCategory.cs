using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopCategory : MonoBehaviour
{
    public string ShopCategoryName;
    public TextMeshProUGUI ShopCategoryNameText;

    public List<ItemData> Items = new();

    private void OnEnable()
    {
        ShopCategoryNameText.text = ShopCategoryName;
    }
}
