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
            ExplanationLog();
            
            DeleteThisSession();
        }
        
        public void Loose()
        {
            title.text = DBManager.Instance.logTexts.loose;
            ExplanationLog();
            
            DeleteThisSession();
        }

        private static void DeleteThisSession()
        {
            PlayerPrefs.DeleteAll();
        }

        private void ExplanationLog()
        {
            Web web = GetComponent<Web>();
            
            for (int i = 0; i < web.allShownTasks.Count; i++)
            {
                explanationLog.text += web.allAnswers[i] switch
                {
                    0 => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerRight,
                    1 => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerMedium,
                    _ => web.allShownTasks[i].question + ": " + web.allShownTasks[i].explanationAnswerWrong
                };
            }
        }
    }
}