using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI changedMoneyText;

    private void Start()
    {
        Actions.OnMoneyChanged += UpdateMoneyAmount;

        moneyText.text = MoneySystem.Currency.ToString() + "$";
    }

    public void UpdateMoneyAmount(float amount)
    {
        moneyText.text = MoneySystem.Currency.ToString() + "$";
    }
}
