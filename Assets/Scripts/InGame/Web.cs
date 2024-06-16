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
        public List<int> allAnswers = new();

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
            if (PlayerPrefs.GetInt("TasksCompleteCount") > 0)
            {
                _surveySectionShownCount = PlayerPrefs.GetInt("SurveySectionShownCount", 0);
                _surveyEnemySectionShownCount = PlayerPrefs.GetInt("SurveyEnemySectionShownCount", 0);
                _softSectionShownCount = PlayerPrefs.GetInt("SoftSectionShownCount", 0);
                _softEnemySectionShownCount = PlayerPrefs.GetInt("SoftEnemySectionShownCount", 0);
                _otherSectionShownCount = PlayerPrefs.GetInt("OtherSectionShownCount", 0);

                int section = PlayerPrefs.GetInt("Section");
                int taskId = PlayerPrefs.GetInt("Task");

                _currentTask = section switch
                {
                    0 => DBManager.Instance.survey[taskId],
                    1 => DBManager.Instance.surveyEnemy[taskId],
                    2 => DBManager.Instance.soft[taskId],
                    3 => DBManager.Instance.softEnemy[taskId],
                    4 => DBManager.Instance.other[taskId],
                    _ => _currentTask
                };
            }
            else
            {
                GenerateTask();
            }
            
            GenerateQuestionWithAnswers();
        }

        private void GenerateTask()
        {
            int chosenSection = ChooseSection(Random.Range(0, 5));
            
            if (chosenSection == -1)
            {
                GetComponent<Game>().GameOvering(true);
                Debug.Log("End of current level");
                return;
            }
            else
            {
                PlayerPrefs.SetInt("Section", chosenSection);
            }

            GenerateQuestionWithAnswers();
        }

        private void GenerateQuestionWithAnswers()
        {
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
                    int taskId = Random.Range(0, DBManager.Instance.survey.Length);
                    _currentTask = DBManager.Instance.survey[taskId];
                    PlayerPrefs.SetInt("Task", taskId);
                    
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
                    int taskId = Random.Range(0, DBManager.Instance.surveyEnemy.Length);
                    _currentTask = DBManager.Instance.surveyEnemy[taskId];
                    PlayerPrefs.SetInt("Task", taskId);
                    
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
                    int taskId = Random.Range(0, DBManager.Instance.soft.Length);
                    _currentTask = DBManager.Instance.soft[taskId];
                    PlayerPrefs.SetInt("Task", taskId);
                    
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
                    int taskId = Random.Range(0, DBManager.Instance.softEnemy.Length);
                    _currentTask = DBManager.Instance.softEnemy[taskId];
                    PlayerPrefs.SetInt("Task", taskId);
                    
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
                    int taskId = Random.Range(0, DBManager.Instance.other.Length);
                    _currentTask = DBManager.Instance.other[taskId];
                    PlayerPrefs.SetInt("Task", taskId);
                    
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
                
                allAnswers.Add(0);
            }
            else if (order == _answerMediumOrder)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerMedium);
                Task.Instance.DecreaseTime(2);
                
                allAnswers.Add(1);
            }
            else if (order == _answerWrongOrder)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerWrong);
                Task.Instance.DecreaseTime(1);
                
                allAnswers.Add(2);
            }

            if (DBManager.Instance.survey.Contains(_currentTask)) Task.Instance.AddWorkWithSurvey();
            else if (DBManager.Instance.soft.Contains(_currentTask)) Task.Instance.AddWorkWithSoft();
            else if (DBManager.Instance.other.Contains(_currentTask)) Task.Instance.AddAchievement(_currentTask.achievement);
            
            allShownTasks.Add(_currentTask);
            
            GenerateTask();
        }
    }
}