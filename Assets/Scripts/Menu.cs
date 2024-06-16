using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject newGame, continueGame;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("TasksCompleteCount", 0) == 0)
        {
            newGame.SetActive(true);
            continueGame.SetActive(false);
            Debug.Log("new");
        }
        else
        {
            newGame.SetActive(false);
            continueGame.SetActive(true);
            Debug.Log("continue");
        }
    }

    public void OnPlayBtnClick() => StartCoroutine(LoadingGame());
    
    public void OnResetBtnClick()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("TasksCompleteCount", 0);
        StartCoroutine(LoadingGame());
    }

    private IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game");
    }
}