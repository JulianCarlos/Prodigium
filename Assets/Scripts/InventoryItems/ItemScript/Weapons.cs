using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Weapons")]
public class Weapons : ItemData
{
    private void Awake()
    {
        itemCategoryType = ItemCategoryType.Weapons;
    }
}
