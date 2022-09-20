using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class Monster : Entity, IDamageable
{
    public bool IsDead => isDead;

    //Properties Fields
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; OnMaxHealthChanged(); } }
    public float Health { get { return maxHealth; } set { maxHealth = value; OnHealthChanged(); } }
    public float HealthRegenerationSpeed { get { return healthRegenerationSpeed; } set { healthRegenerationSpeed = value; OnHealthRegenerationSpeedChanged(); } }
    public float AttackStrength { get { return attackStrenght; } set { attackStrenght = value; OnAttackStrenghtChanged(); } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; OnAttackSpeedChanged(); } }
    public float Armor { get { return armor; } set { armor = value; OnArmorChanged(); } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; OnMovementSpeedChanged(); } }

    //ReadyOnly Fields
    public float KillReward => killReward;

    //Enum
    [SerializeField] protected MonsterType monsterType;

    //Stats
    [Space(15), Header("Stat Settings")]
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float healthRegenerationSpeed;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackStrenght;
    [SerializeField] protected float armor;
    [SerializeField] protected float movementSpeed;
    
    //BodyParts
    [Space(15), Header("Body Parts: ")]
    [SerializeField] private BodyPart head;
    [SerializeField] private BodyPart torso;
    [SerializeField] private BodyPart leftArm;
    [SerializeField] private BodyPart rightArm;
    [SerializeField] private BodyPart leftLeg;
    [SerializeField] private BodyPart rightLeg;
    [SerializeField] private List<BodyPart> bodyParts = new List<BodyPart>();

    //MonsterSettings
    [Space(15), Header("Monster Settings")]
    [SerializeField] protected int monsterLevel;
    [SerializeField] protected float killReward;

    //Damagetypes
    [Space(15)]
    [SerializeField] protected DamageTypes[] weakness;

    [SerializeField] protected bool individualBodyPartHealth = false;

    protected MonsterAI monsterAI;
    protected Animator animator;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        monsterAI = GetComponent<MonsterAI>();
        animator = GetComponent<Animator>();

        if (!individualBodyPartHealth)
            return;

        SetupBody();
        CalculateMaxHealth();
    }

    protected virtual void Start()
    {
        health = maxHealth;
    }

    //Setup
    private void SetupBody()
    {
        bodyParts.Add(head);
        bodyParts.Add(torso);
        bodyParts.Add(leftArm);
        bodyParts.Add(rightArm);
        bodyParts.Add(leftLeg);
        bodyParts.Add(rightLeg);
    }
    private void CalculateMaxHealth()
    {
        float health = 0; 

        foreach(var part in bodyParts)
        {
            if(part)
                health += part.Health;
        }

        maxHealth = health;
    }

    //IDamageable Methods
    public virtual void TakeDamage(DamageTypes[] types, float damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
    public void TakeDamage(DamageTypes types, float damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
    public virtual void ApplyDamage()
    {

    }
    public virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("death");
        Actions.OnMonsterDeath(this);
    }

    //OnVariableChanged Methods
    protected virtual void OnMaxHealthChanged()
    {
        Debug.Log("Max Health Changed");
    }
    protected virtual void OnHealthChanged()
    {

    }
    protected virtual void OnHealthRegenerationSpeedChanged()
    {
        Debug.Log("Changed Regen Speed");
    }
    protected virtual void OnAttackStrenghtChanged()
    {
        Debug.Log("AttackStrenghtChanged");
    }
    protected virtual void OnAttackSpeedChanged()
    {
        Debug.Log("Attack Speed Changed");
    }
    protected virtual void OnArmorChanged()
    {
        Debug.Log("Armor Changed");
    }
    protected virtual void OnMovementSpeedChanged()
    {
        Debug.Log("OnMovementSpeedChanged");
    }


}
