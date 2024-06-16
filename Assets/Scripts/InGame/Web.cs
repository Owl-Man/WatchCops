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
        private List<string> _currentAnswersForChoose = new();

        private int _surveySectionShownCount, _surveyEnemySectionShownCount;
        private int _softSectionShownCount, _softEnemySectionShownCount;
        private int _otherSectionShownCount;

        private const int SurveySectionCount = 3, SurveyEnemySectionCount = 1;
        private const int SoftSectionCount = 4, SoftEnemySectionCount = 2;
        private const int OtherSectionCount = 4;

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
            _currentAnswersForChoose.Clear();
            
            List<string> answers = new();

            answers.Add(_currentTask.answerRight);
            answers.Add(_currentTask.answerMedium);
            answers.Add(_currentTask.answerWrong);

            text.text = _currentTask.question + "\n ----------------------";

            while (_currentAnswersForChoose.Count < answers.Count)
            {
                int variant = Random.Range(0, answers.Count);
                
                while (_currentAnswersForChoose.Contains(answers[variant]))
                {
                    variant = Random.Range(0, answers.Count);
                }
                
                _currentAnswersForChoose.Add(answers[variant]);
                
                text.text += "\n" + _currentAnswersForChoose.Count + ") " + answers[variant];
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
            if (_currentAnswersForChoose[order] == _currentTask.answerRight)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerRight);
                Task.Instance.DecreaseTime(1);
                
                allAnswers.Add(0);
            }
            else if (_currentAnswersForChoose[order] == _currentTask.answerMedium)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerMedium);
                Task.Instance.DecreaseTime(2);
                
                allAnswers.Add(1);
            }
            else if (_currentAnswersForChoose[order] == _currentTask.answerWrong)
            {
                Log.Instance.AddMessage(_currentTask.resultAnswerWrong);
                Task.Instance.DecreaseTime(3);
                
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