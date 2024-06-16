using System;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Text title;
        [SerializeField] private Text explanationLog;

        public void Win()
        {
            title.text = DBManager.Instance.logTexts.win;
            title.text += "\n---";
            ExplanationLog();
            
            DeleteThisSession();
        }
        
        public void Loose()
        {
            title.text = DBManager.Instance.logTexts.loose;
            title.text += "\n---";
            ExplanationLog();
            
            DeleteThisSession();
        }

        private static void DeleteThisSession()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("TasksCompleteCount", 0);
            
            /*PlayerPrefs.SetInt("SurveySectionShownCount", 0);
            PlayerPrefs.SetInt("SurveyEnemySectionShownCount", 0);
            PlayerPrefs.SetInt("SoftSectionShownCount", 0);
            PlayerPrefs.SetInt("SoftEnemySectionShownCount", 0);
            PlayerPrefs.SetInt("OtherSectionShownCount", 0);
            
            PlayerPrefs.SetInt("Time", 0);
            PlayerPrefs.SetInt("WorkWithSurveyCount", 0);
            PlayerPrefs.SetInt("WorkWithSoftCount", 0);*/

            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetString("Ach" + i, "0");
            }
        }

        private void ExplanationLog()
        {
            Web web = GetComponent<Web>();

            if (web.allShownTasks.Count == 0)
            {
                explanationLog.text = "Нет данных за сессию";
            }
            
            for (int i = 0; i < web.allShownTasks.Count; i++)
            {
                explanationLog.text += "\n";
                
                explanationLog.text += web.allAnswers[i] switch
                {
                    0 => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerRight,
                    1 => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerMedium,
                    _ => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerWrong
                };
                
                explanationLog.text += "\n-";
            }
        }
    }
}