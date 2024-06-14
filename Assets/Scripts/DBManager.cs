using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    private SurveySection[] survey;
    public SurveySection[] surveyEnemy;
    public List<string> soft = new();
    public List<string> softEnemy = new();
    public List<string> other = new();

    private void Awake()
    {
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
        
        survey = loadedData.survey;
        surveyEnemy = loadedData.surveyEnemy;

        Debug.Log(survey[0].question);
        Debug.Log(survey[0].answerRight);
        Debug.Log(survey[0].answerMedium);
        Debug.Log(surveyEnemy[0].question);
        Debug.Log(surveyEnemy[0].answerMedium);
    }
}