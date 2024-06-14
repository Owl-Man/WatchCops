using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    public LogTexts logTexts;
    
    public SurveySection[] survey;
    public SurveySection[] surveyEnemy;
    public SurveySection[] soft;
    public SurveySection[] softEnemy;
    public SurveySection[] other;

    public static DBManager Instance;

    private void Awake()
    {
        Instance = this;
        
        StartCoroutine(LoadData());

        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LoadData()
    {
        string path = Application.streamingAssetsPath + "/db.json";
        
        using UnityWebRequest webRequest = UnityWebRequest.Get(path);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Raw JSON: " + webRequest.downloadHandler.text);
            ProcessJson(webRequest.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Failed to download JSON: " + webRequest.error);
        }
        
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download JSON: " + webRequest.error);
        }
    }

    private void ProcessJson(string jsonData)
    {
        Task loadedData = JsonUtility.FromJson<Task>(jsonData);

        logTexts = loadedData.logTexts[0];
        
        survey = loadedData.survey;
        surveyEnemy = loadedData.surveyEnemy;
        soft = loadedData.soft;
        softEnemy = loadedData.softEnemy;
        other = loadedData.other;
        
        Debug.Log(logTexts.enter);

        Debug.Log(survey[0].question);
        Debug.Log(surveyEnemy[0].question);
        Debug.Log(softEnemy[0].question);
        Debug.Log(soft[0].question);
        Debug.Log(other[0].question);
    }
}