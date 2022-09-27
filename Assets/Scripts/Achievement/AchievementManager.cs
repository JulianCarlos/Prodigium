using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType
{
    Kill,
    Collect,
    Find
}

public enum AchievementDifficulty
{
    Easy,
    Medium,
    Hard,
    Apocalyptic
}

public static class AchievementManager
{
    public static List<Achievement> CompletedAchievements = new List<Achievement>();

    public static List<Achievement> CreateAchievements(int numberOfAchievements, AchievementUI uiPrefab)
    {
        var achievments = new List<Achievement>();

        for (int i = 0; i < numberOfAchievements; i++)
        {
            AchievementType randomAchievementType = (AchievementType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(AchievementType)).Length);

            Achievement achievement = new Achievement(randomAchievementType, uiPrefab);
            achievments.Add(achievement);
        }
        return achievments;
    }

    public static void CompleteAchievement(Achievement achievement)
    {
        CompletedAchievements.Add(achievement);
    }
}

