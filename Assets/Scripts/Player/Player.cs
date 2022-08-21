using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, ISaveable<object>
{
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; OnMaxHealthChanged(); } }
    public float Health { get { return health; } set { health = value; OnHealthChanged(); } }
    public float MeleeDamage { get { return meleeDamage; } set { meleeDamage = value; OnMeleeDamageChanged(); } }
    public float Armor { get { return armor; } set { armor = value; OnArmorChanged(); } }

    [Header("Player Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float meleeDamage;
    [SerializeField] private float armor;

    [Header("Level Settings")]
    [SerializeField] private int level = 1;
    [SerializeField] private float currentExperience = 0;
    [SerializeField] private float experienceForNextLevel = 1000f;
    [SerializeField] private float levelUpExpMultiplier = 1.15f;

    void Start()
    {
        Health = MaxHealth;
    }

    //Level Methods
    private void LevelUp()
    {
        level++;
        experienceForNextLevel += (experienceForNextLevel * levelUpExpMultiplier);
    }
    private void AddExperience(float exp)
    {
        var expGranted = exp;

        while(expGranted > 0)
        {
            float experienceNeededForLevelup = experienceForNextLevel - expGranted;

            if(expGranted < experienceNeededForLevelup)
            {
                currentExperience += expGranted;
                break;
            }
            else
            {
                currentExperience += experienceNeededForLevelup;
                expGranted -= experienceNeededForLevelup;
                LevelUp();
            }
        }
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
        //Actions.OnPlayerTakeFallDamage(this, damage);
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
        //Actions.OnPlayerDeath(this);
    }

    //Save Methods
    public object CaptureState()
    {
        return new SaveData()
        {
            level = level,
            experience = currentExperience,
            experienceForNextLevel = experienceForNextLevel
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        level = saveData.level;
        currentExperience = saveData.experience;
        experienceForNextLevel = saveData.experienceForNextLevel;
    }

    [Serializable]
    internal struct SaveData
    {
        internal int level;
        internal float experience;
        internal float experienceForNextLevel;
    }
}
