using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCategory : MonoBehaviour
{
    public List<Objects> ShopCategoryObjects => shopCategoryObjects;
    [SerializeField] private List<Objects> shopCategoryObjects;

    public string ShopCategoryName;
    public TextMeshProUGUI ShopCategoryNameText;

    private void OnEnable()
    {
        ShopCategoryNameText.text = ShopCategoryName;
    }
}
