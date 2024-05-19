using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuBack1, menuBack2;
    
    public void OnPlayBtnClick()
    {
        menuBack1.SetActive(false);
        menuBack2.SetActive(true);

        StartCoroutine(LoadingGame());
    }

    private IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game");
    }
}