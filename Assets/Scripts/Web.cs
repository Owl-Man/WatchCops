using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    [SerializeField] private Text text;

    private SurveySection _currentTask;

    private int _answerRightOrder;
    private int _answerMediumOrder;
    private int _answerWrongOrder;

    private void Start()
    {
        GenerateTask();
    }

    private void GenerateTask()
    {
        int chosenSection = Random.Range(0, 3);

        if (chosenSection == 0)
        {
            _currentTask = DBManager.Instance.survey[Random.Range(0, DBManager.Instance.survey.Length)];
        }
        else if (chosenSection == 1)
        {
            _currentTask = DBManager.Instance.soft[Random.Range(0, DBManager.Instance.survey.Length)];
        }
        else if (chosenSection == 2)
        {
            _currentTask = DBManager.Instance.other[Random.Range(0, DBManager.Instance.survey.Length)];
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

    public void OnAnsBtnClick(int order) => ChosenAnswer(order);
    
    private void ChosenAnswer(int order) 
    {
        if (order == _answerRightOrder)
        {
            Log.Instance.AddMessage(_currentTask.resultAnswerRight);
        }
        else if (order == _answerMediumOrder)
        {
            Log.Instance.AddMessage(_currentTask.resultAnswerMedium);
        }
        else if (order == _answerWrongOrder)
        {
            Log.Instance.AddMessage(_currentTask.resultAnswerWrong);
        }
        
        GenerateTask();
    }
}