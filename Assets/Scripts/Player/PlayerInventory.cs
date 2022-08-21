using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventoryItems = new List<Item>();

    [SerializeField] private Item itemInUse;

    private void Start()
    { 

    }
}
