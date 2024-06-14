using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class QuestionData
{
    public string question;
    public string answerRight;
    public string answerMedium;
    public string answerWrong;
    public string resultAnswerRight;
    public string resultAnswerMedium;
    public string resultAnswerWrong;
    public string explanationAnswerRight;
    public string explanationAnswerMedium;
    public string explanationAnswerWrong;
}

[System.Serializable]
public class SurveySection
{
    public string[] questionData;
}

[System.Serializable]
public class Task
{
    public SurveySection[] survey;
    public SurveySection[] surveyEnemy;
}