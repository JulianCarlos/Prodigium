using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int maxAchievementAmount = 3;

    //Achievement
    [SerializeField] private List<Achievement> activeAchievements = new List<Achievement>();
    [SerializeField] private Transform achievementContent;
    [SerializeField] private AchievementUI achievementUIPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CreateAchievements(maxAchievementAmount);
    }

    private void CreateAchievements(int amount)
    {
        activeAchievements = AchievementManager.CreateAchievements(amount, achievementUIPrefab);

        foreach (var achievement in activeAchievements)
        {
            achievement.Child = Instantiate(achievementUIPrefab, achievementContent);
            achievement.Child.SetValues(achievement);
        }
    }
}
