using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string AchievementName;
    public string AchievementDescription;

    public int CurrentAchievementAmount;
    public int MaxAchievementAmount;

    public AchievementType AchievementType;
    public AchievementDifficulty AchievementDifficulty;

    public float MoneyReward;

    public AchievementUI Child;

    public bool IsCompleted;

    public Achievement(AchievementType type, AchievementUI uiPrefab)
    {
        Child = uiPrefab;
        CurrentAchievementAmount = 0;

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
        AchievementDescription = "Kill target test";
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
        if (IsCompleted)
            return;

        ConditionCheck();

        Child.SetValues(this);
    }

    private void OnCollectedItem(Item item)
    {
        if (IsCompleted)
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
