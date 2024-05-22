using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject mainPage, loadingPage, logPage, webPage, taskPage, gameOverPage, exitPage;

    public void OnPageBtnClick(int id)
    {
        switch (id)
        {
            case 0:
                StartCoroutine(OpenPage(mainPage));
                break;
            case 1:
                StartCoroutine(OpenPage(logPage));
                break;
            case 2:
                StartCoroutine(OpenPage(webPage));
                break;
            case 3:
                StartCoroutine(OpenPage(taskPage));
                break;
            case 4:
                StartCoroutine(OpenPage(exitPage));
                break;
        }
    }

    public void OnQuitBtnClick() => Application.Quit();

    private IEnumerator OpenPage(GameObject page)
    {
        loadingPage.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        loadingPage.SetActive(false);
        page.SetActive(true);
    }
}