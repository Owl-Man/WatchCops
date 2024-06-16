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
            if (PlayerPrefs.GetInt("TasksCompleteCount") > 0)
            {
                _time = PlayerPrefs.GetInt("Time", 24);
                _workWithSurveyCount = PlayerPrefs.GetInt("WorkWithSurveyCount", 0);
                _workWithSoftCount = PlayerPrefs.GetInt("WorkWithSoftCount", 0);

                for (int i = 0; i < 4; i++)
                {
                    if (PlayerPrefs.GetString("Ach" + i, "0") != "0")
                    {
                        _achievementsList.Add(PlayerPrefs.GetString("Ach" + i));
                    }
                }
            }
            
            UpdateStatsText();
            UpdateAchievementText();
        }

        public void DecreaseTime(int decreaseTime)
        {
            _time -= decreaseTime;
            
            PlayerPrefs.SetInt("Time", _time);
            
            if (_time <= 0) GetComponent<Game>().GameOvering(false);
            
            UpdateStatsText();
        }

        public void AddWorkWithSurvey()
        {
            _workWithSurveyCount++;
            PlayerPrefs.SetInt("WorkWithSurveyCount", _workWithSurveyCount);
            UpdateStatsText();
        }

        public void AddWorkWithSoft()
        {
            _workWithSoftCount++;
            PlayerPrefs.SetInt("WorkWithSoftCount", _workWithSoftCount);
            UpdateStatsText();
        }

        public void AddAchievement(string achievement)
        {
            _achievementsList.Add(achievement);
            PlayerPrefs.SetString("Ach" + (_achievementsList.Count - 1), achievement);
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