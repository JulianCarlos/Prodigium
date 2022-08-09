using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCategory : MonoBehaviour
{
    public List<Objects> Items = new();

    public string ShopCategoryName;
    public TextMeshProUGUI ShopCategoryNameText;

    private void OnEnable()
    {
        ShopCategoryNameText.text = ShopCategoryName;
    }
}
