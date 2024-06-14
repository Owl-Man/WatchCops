using System;
using TMPro;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        AddMessage(DBManager.Instance.logTexts.enter);
    }

    private void AddMessage(string message)
    {
        if (!String.IsNullOrEmpty(text.text)) text.text += "\n";
        
        text.text += message;
    }
}