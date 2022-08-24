using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMan : Monster
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
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
