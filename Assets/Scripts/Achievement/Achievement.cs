using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string AchievementName { get; private set; }
    public string AchievementDescription { get; private set; }

    public int CurrentAchievementAmount { get; private set; }
    public int MaxAchievementAmount { get; private set; }

    public AchievementType AchievementType { get; private set; }
    public AchievementDifficulty AchievementDifficulty { get; private set; }

    public float MoneyReward { get; private set; }

    internal AchievementUI Child;

    public bool IsCompleted { get; private set; }

    [SerializeField] private Type monsterType;
    [SerializeField] private Type itemType;
    [SerializeField] private Vector3 locationType;

    public Achievement(AchievementType type, AchievementUI uiPrefab)
    {
        Child = uiPrefab;
        CurrentAchievementAmount = 0;

        monsterType = AchievementDatabase.GetRandomMonsterType();
        SelectAchievement(type);

        Child.SetValues(this);
    }

    private void SelectAchievement(AchievementType type)
    {
        switch (type)
        {
            case AchievementType.Kill:
                Actions.OnMonsterDeath += OnMonsterKill;
                SetKillAchievement();
                break;

            case AchievementType.Collect:
                Actions.OnItemCollected += OnCollectedItem;
                SetCollectAchievement();
                break;

            case AchievementType.Find:
                Actions.OnLocationArrived += OnLocationArrived;
                SetFindAchievement();
                break;
        }
    }

    private void SetKillAchievement()
    {
        MaxAchievementAmount = UnityEngine.Random.Range(1, 4);
        MoneyReward = UnityEngine.Random.Range(100, 250) * MaxAchievementAmount;
        AchievementType = AchievementType.Kill;
        AchievementDifficulty = AchievementDifficulty.Medium;
        AchievementName = $"Kill Target: {MoneyReward}$ ({AchievementDifficulty.ToString()})";
        AchievementDescription = $"Kill {MaxAchievementAmount} {monsterType.Name}";
    }

    private void SetCollectAchievement()
    {
        MaxAchievementAmount = UnityEngine.Random.Range(1, 4);
        MoneyReward = UnityEngine.Random.Range(100, 250)  * MaxAchievementAmount;
        AchievementType = AchievementType.Collect;
        AchievementDifficulty = AchievementDifficulty.Easy;
        AchievementName = $"Collect Items: {MoneyReward}$ ({AchievementDifficulty.ToString()})";
        AchievementDescription = "Collect items test";
    }

    private void SetFindAchievement()
    {
        MaxAchievementAmount = 1;
        MoneyReward = UnityEngine.Random.Range(100, 250);
        AchievementType = AchievementType.Find;
        AchievementDifficulty = AchievementDifficulty.Easy;
        AchievementName = $"Find Place: {MoneyReward}$ ({AchievementDifficulty.ToString()})";
        AchievementDescription = "Find this Place: Test";
    }

    private void OnMonsterKill(Monster monster)
    {
        if (IsCompleted || monster.GetType() != monsterType)
            return;

        ConditionCheck();

        Child.SetValues(this);
    }

    private void OnCollectedItem(Item item)
    {
        if (IsCompleted || item.GetType() != itemType)
            return;

        ConditionCheck();

        Child.SetValues(this);
    }

    private void OnLocationArrived(Vector3 place)
    {
        if (IsCompleted)
            return;

        ConditionCheck();

        Child.SetValues(this);
    }

    private void ConditionCheck()
    {
        CurrentAchievementAmount++;

        if (CurrentAchievementAmount >= MaxAchievementAmount)
        {
            AchievementManager.CompleteAchievement(this);
            MoneySystem.AddMoney(MoneyReward);
            IsCompleted = true;
        }
    }
}
