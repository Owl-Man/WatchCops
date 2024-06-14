using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject mainPage, loadingPage, logPage, webPage, taskPage, gameOverPage, exitPage;

    [SerializeField] private CanvasScaler canvasScaler;

    private bool _isStarted;
    private void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(0.7f);
        logPage.SetActive(true);

        _isStarted = true;
    }

    public void OnPageBtnClick(int id)
    {
        if (!_isStarted) return;
        
        StopAllCoroutines();
        
        switch (id)
        {
            case 0:
                StartCoroutine(OpenPage(mainPage));
                StartCoroutine(ZoomAnimationOut());
                break;
            case 1:
                StartCoroutine(OpenPage(logPage));
                StartCoroutine(ZoomAnimationIn());
                break;
            case 2:
                StartCoroutine(OpenPage(webPage));
                StartCoroutine(ZoomAnimationIn());
                break;
            case 3:
                StartCoroutine(OpenPage(taskPage));
                StartCoroutine(ZoomAnimationIn());
                break;
            case 4:
                StartCoroutine(OpenPage(exitPage));
                StartCoroutine(ZoomAnimationIn());
                break;
        }
    }

    public void OnQuitBtnClick() => Application.Quit();

    private IEnumerator OpenPage(GameObject page)
    {
        loadingPage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loadingPage.SetActive(false);
        page.SetActive(true);
    }

    private IEnumerator ZoomAnimationIn()
    {
        while (canvasScaler.scaleFactor < 1.06f)
        {
            canvasScaler.scaleFactor += 0.0012f;
            yield return null;
        }
    }
    
    private IEnumerator ZoomAnimationOut()
    {
        while (canvasScaler.scaleFactor > 1f)
        {
            canvasScaler.scaleFactor -= 0.0012f;
            yield return null;
        }
    }
}