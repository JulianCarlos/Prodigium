using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Monster
{
    protected override void Start()
    {
        base.Start();
        monsterType = MonsterType.Ghoul;
    }

    public override void TakeDamage(DamageTypes[] types, float damage)
    {
        base.TakeDamage(types, damage);
    }

    public override void ApplyDamage()
    {
        base.ApplyDamage();
    }

    public override void Die()
    {
        base.Die();
    }
}
