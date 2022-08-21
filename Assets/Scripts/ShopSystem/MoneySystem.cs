using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneySystem
{
    public static float Currency { get => currency; set => currency = value; }
    private static float currency = 10000;

    public static float CurrencyMultiplier => currencyMultiplier;
    private static float currencyMultiplier = 1;

    public static void AddMoney(float amount)
    {
        Currency += amount * CurrencyMultiplier;
        
        Actions.OnMoneyChanged(amount);
    }

    public static void RemoveMoney(float amount)
    {
        if (Currency <= 0)
            return;

        if((Currency - amount) < 0) 
            Currency = 0;
        else 
            Currency -= amount;

        Actions.OnMoneyChanged(amount);
    }

    public static bool MoneyCheck(float amount)
    {
        return Currency - amount > 0;
    }
}
