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
    void Update()
    {
        
    }

    //OnVariableChanged Methods
    private void OnMaxHealthChanged()
    {

    }
    private void OnHealthChanged()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    private void OnMeleeDamageChanged()
    {

    }
    private void OnArmorChanged()
    {

    }

    //TakeDamage
    public void TakeFallDamage(float damage)
    {
        Health -= damage;
        //Actions.OnPlayerTakeFallDamage(this, damage);
    }
    public void TakeDamage(DamageTypes[] types, float damage)
    {
        Actions.OnTakeDamage(this);
    }
    public void ApplyDamage()
    {

    }
    public void Die()
    {
        Debug.Log("You died");
        Actions.OnPlayerDeath(this);
    }
}
