using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementNameText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;
    [SerializeField] private TextMeshProUGUI achievementAmountText;

    [SerializeField] private Image checkmarkImage;

    //[SerializeField] private AchievementType achievementType;
    //[SerializeField] private AchievementDifficulty achievementDifficulty;

    public void SetValues(Achievement achievement)
    {
        achievementNameText.text = achievement.AchievementName;
        achievementDescriptionText.text = achievement.AchievementDescription;

        //achievementType = achievement.AchievementType;
        //achievementDifficulty = achievement.AchievementDifficulty;

        achievementAmountText.text = $"{achievement.CurrentAchievementAmount} / {achievement.MaxAchievementAmount}";

        checkmarkImage.gameObject.SetActive(achievement.IsCompleted);
    }
}
