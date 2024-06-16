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
        }
        
        public void Loose()
        {
            title.text = DBManager.Instance.logTexts.loose;
        }

        private void ExplanationLog()
        {
            
        }
    }
}