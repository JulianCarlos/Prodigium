using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMan : Monster
{
    [SerializeField] private Color FriendlyColor;
    [SerializeField] private Color AngryColor;

    [SerializeField] private float intensitivy;

    [SerializeField] private Material PumpkinMaterial;

    protected override void Awake()
    {
        base.Awake();
        PumpkinMaterial.SetColor("_EmissionColor", FriendlyColor * intensitivy);
    }

    protected override void Start()
    {
        base.Start();
        monsterType = MonsterType.PumpkinMan;
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
