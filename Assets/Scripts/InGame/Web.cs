using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class Web : MonoBehaviour
    {
        [SerializeField] private Text text;

        public List<SurveySection> allShownTasks = new();

        private SurveySection _currentTask;

        private int _surveySectionShownCount, _surveyEnemySectionShownCount;
        private int _softSectionShownCount, _softEnemySectionShownCount;
        private int _otherSectionShownCount;

        private const int SurveySectionCount = 3, SurveyEnemySectionCount = 1;
        private const int SoftSectionCount = 4, SoftEnemySectionCount = 2;
        private const int OtherSectionCount = 4;

        private int _answerRightOrder, _answerMediumOrder, _answerWrongOrder;

        private void Start()
        {
            GenerateTask();
        }

        private void GenerateTask()
        {
            if (ChooseSection(Random.Range(0, 5)) == -1)
            {
                GetComponent<Game>().GameOvering(true);
                Debug.Log("End of current level");
                return;
            }

            List<string> answers = new List<string>();
        
            answers.Add(_currentTask.answerRight);
            answers.Add(_currentTask.answerMedium);
            answers.Add(_currentTask.answerWrong);

            text.text = _currentTask.question + "\n ----------------------";

            while (answers.Count > 0)
            {
                int variant = Random.Range(0, answers.Count);
                text.text += "\n" + (4 - answers.Count) + ") " + answers[variant];

                if (answers[variant] == _currentTask.answerRight)
                {
                    _answerRightOrder = variant;
                }
                else if (answers[variant] == _currentTask.answerMedium)
                {
                    _answerMediumOrder = variant;
                }
                else if (answers[variant] == _currentTask.answerWrong)
                {
                    _answerWrongOrder = variant;
                }
            
                answers.Remove(answers[variant]);
            }
        }

        private int ChooseSection(int chosenSection)
        {
            if (chosenSection == 0)
            {
                if (_surveySectionShownCount > SurveySectionCount)
                {
                    chosenSection++;
                }
                else
                {
                    _currentTask = DBManager.Instance.survey[Random.Range(0, DBManager.Instance.survey.Length)];
                    _surveySectionShownCount++;
                    PlayerPrefs.SetInt("SurveySectionShownCount", _surveySectionShownCount);
                }
            }

            if (chosenSection == 1)
            {
                if (_surveyEnemySectionShownCount > SurveyEnemySectionCount)
                {
                    chosenSection++;
                }
                else
                {
                    _currentTask = DBManager.Instance.surveyEnemy[Random.Range(0, DBManager.Instance.surveyEnemy.Length)];
                    _surveyEnemySectionShownCount++;
                    PlayerPrefs.SetInt("SurveyEnemySectionShownCount", _surveyEnemySectionShownCount);
                }
            }

            if (chosenSection == 2)
            {
                if (_softSectionShownCount > SoftSectionCount)
                {
                    chosenSection++;
                }
                else
                {
                    _currentTask = DBManager.Instance.soft[Random.Range(0, DBManager.Instance.soft.Length)];
                    _softSectionShownCount++;
                    PlayerPrefs.SetInt("SoftSectionShownCount", _softSectionShownCount);
                }
            }

            if (chosenSection == 3)
            {
                if (_softEnemySectionShownCount > SoftEnemySectionCount)
                {
                    chosenSection++;
                }
                else
                {
                    _currentTask = DBManager.Instance.softEnemy[Random.Range(0, DBManager.Instance.softEnemy.Length)];
                    _softEnemySectionShownCount++;
                    PlayerPrefs.SetInt("SoftEnemySectionShownCount", _softEnemySectionShownCount);
                }
            }

            if (chosenSection == 4)
            {
                if (_surveySectionShownCount > SurveySectionCount && _surveyEnemySectionShownCount >=
                                                                  SurveyEnemySectionCount
                                                                  && _softSectionShownCount >= SoftSectionCount
                                                                  && _softEnemySectionShownCount >= SoftEnemySectionCount
                                                                  && _otherSectionShownCount >= OtherSectionCount)
                {
                    return -1;
                }
            
                if (_otherSectionShownCount >= OtherSectionCount)
                {
                    return ChooseSection(0);
                }
                else
                {
                    _currentTask = DBManager.Instance.other[Random.Range(0, DBManager.Instance.other.Length)];
                    _otherSectionShownCount++;
                    PlayerPrefs.SetInt("OtherSectionShownCount", _otherSectionShownCount);
                }
            }

            PlayerPrefs.SetInt("TasksCompleteCount", PlayerPrefs.GetInt("TasksCompleteCount") + 1);

            return chosenSection;
        }

        public void OnAnsBtnClick(int order) => ChosenAnswer(order);
    
        private void ChosenAnswer(int order) 
        {
            if (order == _answerRightOrder)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerRight);
                Task.Instance.DecreaseTime(3);
            }
            else if (order == _answerMediumOrder)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerMedium);
                Task.Instance.DecreaseTime(2);
            }
            else if (order == _answerWrongOrder)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerWrong);
                Task.Instance.DecreaseTime(1);
            }

            if (DBManager.Instance.survey.Contains(_currentTask)) Task.Instance.AddWorkWithSurvey();
            else if (DBManager.Instance.soft.Contains(_currentTask)) Task.Instance.AddWorkWithSoft();
            else if (DBManager.Instance.other.Contains(_currentTask)) Task.Instance.AddAchievement(_currentTask.achievement);
            
            allShownTasks.Add(_currentTask);

            GenerateTask();
        }
    }
}