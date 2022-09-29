using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        ammoText.text = string.Empty;
    }

    private void OnEnable()
    {
        Actions.OnItemChanged += SetAmmoValues;
        Actions.OnAmmoChanged += UpdateAmmoValue;
    }

    private void OnDisable()
    {
        Actions.OnItemChanged -= SetAmmoValues;
        Actions.OnAmmoChanged -= UpdateAmmoValue;
    }

    public void SetAmmoValues(ItemData selectedItem)
    {
        if (!selectedItem)
        {
            ammoText.text = string.Empty;
            return;
        }

        if (selectedItem.IngameItem.TryGetComponent(out Weapon weapon))
        {
            UpdateAmmoValue(weapon);
        }
        else
        {
            ammoText.text = string.Empty;
        }
    }

    public void UpdateAmmoValue(Weapon weapon)
    {
        ammoText.text = $"{weapon.CurrentMagazineCapacity} / {weapon.BackupAmmo}";
    }
}
