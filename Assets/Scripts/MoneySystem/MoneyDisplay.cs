using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI changedMoneyText;

    private Vector3 originalChangedMoneyTextPos;

    private void Awake()
    {
        Actions.OnMoneyChanged += UpdateMoneyAmount;

        originalChangedMoneyTextPos = changedMoneyText.transform.position;
    }

    private void Start()
    {
        moneyText.text = MoneySystem.Currency.ToString() + "$";
    }

    private void UpdateMoneyAmount(float amount)
    {
        moneyText.text = MoneySystem.Currency.ToString() + "$";
        changedMoneyText.DOKill();
        StopAllCoroutines();
        StartCoroutine(ChangeMoneyAmount(amount));
    }

    private IEnumerator ChangeMoneyAmount(float amount)
    {
        changedMoneyText.alpha = 1;
        changedMoneyText.transform.position = originalChangedMoneyTextPos;
        changedMoneyText.text = ((amount >= 0) ? "+" : "") + amount.ToString() + "$";
        yield return new WaitForSeconds(2f);
        Tween tween = changedMoneyText.DOFade(0, 2);
        yield return new WaitForSeconds(2f);
        changedMoneyText.text = string.Empty;
    }
}
