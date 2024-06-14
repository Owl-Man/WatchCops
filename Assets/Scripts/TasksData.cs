[System.Serializable]
public struct SurveySection
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
public struct Task
{
    public SurveySection[] survey;
    public SurveySection[] surveyEnemy;
}