using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneySystem
{
    public static float Currency = 10000;

    public static float CurrencyMultiplier = 1;

    public static void AddMoney(float amount)
    {
        Currency += amount * CurrencyMultiplier;
        
        Actions.OnMoneyAdded(amount);

        Debug.Log(Currency);
    }

    public static void RemoveMoney(float amount)
    {
        if (Currency <= 0)
            return;

        if((Currency - amount) < 0) 
            Currency = 0;
        else 
            Currency -= amount;

        Debug.Log(Currency);
    }

    public static bool MoneyCheck(float amount)
    {
        return Currency - amount > 0;
    }
}
