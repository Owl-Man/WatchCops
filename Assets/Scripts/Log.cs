using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] private Text text;

    public static Log Instance;

    private void Awake() => Instance = this;

    private void Start()
    {
        AddMessage(DBManager.Instance.logTexts.enter);
    }

    public void AddMessage(string message)
    {
        if (!String.IsNullOrEmpty(text.text))
        {
            text.text += "\n";
            text.text += "------------------";
            text.text += "\n";
        }
        else
        {
            text.text += "-------------------------------------------------------------------------------------";
        }
        
        text.text += message;
    }
}