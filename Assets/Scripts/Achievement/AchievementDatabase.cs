using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class AchievementDatabase
{
    public static List<Type> MonsterTypes = new List<Type>()
    { 
        typeof(Ghoul), 
        typeof(Zombie), 
        typeof(Vampire) 
    };

    public static List<Type> ItemTypes = new List<Type>()
    {

    };

    public static void AddItem(ItemData item)
    {
        ItemTypes.Add(item.GetType());
    }

    public static Type GetRandomMonsterType()
    {
        var randomMonster = UnityEngine.Random.Range(0, MonsterTypes.Count -1);
        return MonsterTypes[randomMonster];
    }
}
