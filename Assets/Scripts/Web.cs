using TMPro;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private SurveySection _currentTask;

    private void Start()
    {
        GenerateQuestion();
    }

    private void GenerateQuestion()
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

        text.text = _currentTask.question + "\n \n" + _currentTask.answerRight + "\n" + _currentTask.answerMedium + "\n" +
                    _currentTask.answerWrong;
    }

    public void OnFirstAnsBtnClick()
    {
        
    }
}