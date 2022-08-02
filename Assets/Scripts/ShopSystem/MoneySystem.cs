using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneySystem
{
    public static float Currency = 0;

    public static float CurrencyMultiplier = 1;

    public static void AddMoney(float amount)
    {
        Currency += amount;
<<<<<<< Updated upstream
        
        Actions.OnMoneyAdded(amount);
=======


        Debug.Log(Currency);
>>>>>>> Stashed changes
    }

    public static void RemoveMoney(float amount)
    {
        if (Currency <= 0)
            return;

        if((Currency - amount) < 0) 
            Currency = 0;
        else 
            Currency -= amount;

<<<<<<< Updated upstream
        Actions.OnMoneyRemoved(amount);
=======

        Debug.Log(Currency);
>>>>>>> Stashed changes
    }
}
