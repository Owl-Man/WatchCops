using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    private SurveySection[] quiz;
    public List<string> quizEnemy = new();
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
        /*string dataAsJson;

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);

            while (!reader.isDone) { }

            dataAsJson = reader.text;

        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }

        Task loadedData = JsonUtility.FromJson<Task>(dataAsJson);*/

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
        
        quiz = loadedData.survey;

        Debug.Log(quiz[0].questionData[0]);

        /*foreach (var survey in loadedData.survey)
        {
            foreach (var questionGroup in survey.questions)
            {
                foreach (var question in questionGroup.Value)
                {
                    Debug.Log($"Question: {question.question}");
                    // Access other properties similarly
                }
            }
        }*/

        //Debug.Log(quiz["1"].questions["Вопрос"]);
        /*for (int i = 0; i < loadedData.soft[0].data.Length; i++)
        {
            quiz.Add(loadedData.soft[0].data[i]);
        }
        for (int i = 0; i < loadedData.softEnemy[0].data.Length; i++)
        {
            quiz.Add(loadedData.softEnemy[0].data[i]);
        }
        for (int i = 0; i < loadedData.other[0].data.Length; i++)
        {
            quiz.Add(loadedData.other[0].data[i]);
        }*/
    }
}