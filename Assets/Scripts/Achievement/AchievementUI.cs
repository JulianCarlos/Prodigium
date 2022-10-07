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

    public void SetValues(Achievement achievement)
    {
        achievementNameText.text = achievement.AchievementName;
        achievementDescriptionText.text = achievement.AchievementDescription;

        achievementAmountText.text = $"{achievement.CurrentAchievementAmount} / {achievement.MaxAchievementAmount}";

        checkmarkImage.gameObject.SetActive(achievement.IsCompleted);
    }
}
