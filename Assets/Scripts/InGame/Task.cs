using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private Text stats, achievements;

        private int _time = 24;
        private int _workWithSurveyCount;
        private int _workWithSoftCount;

        private List<string> _achievementsList = new();

        public static Task Instance;

        private void Awake() => Instance = this;

        private void Start()
        {
            UpdateStatsText();
            UpdateAchievementText();
        }

        public void DecreaseTime(int decreaseTime)
        {
            _time -= decreaseTime;
            UpdateStatsText();
        }

        public void AddWorkWithSurvey()
        {
            _workWithSurveyCount++;
            UpdateStatsText();
        }

        public void AddWorkWithSoft()
        {
            _workWithSoftCount++;
            UpdateStatsText();
        }

        public void AddAchievement(string achievement)
        {
            _achievementsList.Add(achievement);
            UpdateAchievementText();
        }

        private void UpdateStatsText()
        {
            stats.text = "Прошло времени: " + (24 - _time) + "/24ч (осталось " + _time + "ч)\n";
            stats.text += "Работа с опросами " + _workWithSurveyCount + "/3 \n";
            stats.text += "Работа с изображениями и текстом" + _workWithSoftCount + "/4 \n";
            stats.text += "---";
        }

        private void UpdateAchievementText()
        {
            achievements.text = "Достижения:";

            for (int i = 0; i < _achievementsList.Count; i++)
            {
                achievements.text += "\n" + _achievementsList[i];
            }
        }
    }
}