using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        Actions.OnItemChanged += SetAmmoValues;
        Actions.OnAmmoChanged += UpdateAmmoValue;

        ammoText.text = string.Empty;
    }

    public void SetAmmoValues(ItemData selectedItem)
    {
        if (!selectedItem)
        {
            ammoText.text = string.Empty;
            return;
        }

        if (selectedItem.PreviewItem.TryGetComponent(out Weapon weapon))
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
