using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingCakePiece : MonoBehaviour
{
    public Image Icon;
    public Image CakePiece;

    public Vector3 OriginalPos;

    [SerializeField] private List<ItemData> categoryItems;

    [SerializeField] private ItemData currentSelectedItem;
    [SerializeField] private int currentSelectedItemIndex;

    private void Awake()
    {
        //Fill List
    }

    private void Start()
    {
        OriginalPos = transform.localPosition;

        currentSelectedItemIndex = 0;

        if (categoryItems != null)
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
    }

    public void ScrollPrevious()
    {
        if (categoryItems != null && currentSelectedItemIndex != 0)
        {
            currentSelectedItemIndex--;
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
        }
    }

    public void ScrollNext()
    {
        if (categoryItems != null && currentSelectedItemIndex != categoryItems.Count - 1)
        {
            currentSelectedItemIndex++;
            currentSelectedItem = categoryItems[currentSelectedItemIndex];
        }
    }
}
