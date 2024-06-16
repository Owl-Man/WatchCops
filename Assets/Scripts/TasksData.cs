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
    public string achievement;
}

[System.Serializable]
public struct LogTexts
{
    public string enter;
    public string win;
    public string loose;
}

[System.Serializable]
public struct Task
{
    public LogTexts[] logTexts;
    public SurveySection[] survey;
    public SurveySection[] surveyEnemy;
    public SurveySection[] soft;
    public SurveySection[] softEnemy;
    public SurveySection[] other;
}