using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; OnMaxHealthChanged(); } }
    public float Health { get { return health; } set { health = value; OnHealthChanged(); } }
    public float MeleeDamage { get { return meleeDamage; } set { meleeDamage = value; OnMeleeDamageChanged(); } }
    public float Armor { get { return armor; } set { armor = value; OnArmorChanged(); } }

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float meleeDamage;
    [SerializeField] private float armor;

    void Start()
    {
        Health = MaxHealth;
    }

    //OnVariableChanged Methods
    private void OnMaxHealthChanged()
    {

    }
    private void OnHealthChanged()
    {

    }
    private void OnMeleeDamageChanged()
    {

    }
    private void OnArmorChanged()
    {

    }

    //Checks
    private void DeathCheck()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    //TakeDamage
    public void TakeFallDamage(float damage)
    {
        Actions.OnPlayerTakeFallDamage(this, damage);
        Health -= damage;

        DeathCheck();
    }
    public void TakeDamage(DamageTypes[] types, float damage)
    {
        Actions.OnTakeDamage(this);
        DeathCheck();
    }
    public void ApplyDamage()
    {

    }
    public void Die()
    {
        Actions.OnPlayerDeath(this);
    }
}
