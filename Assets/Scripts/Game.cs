using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject mainPage, loadingPage, logPage, webPage, taskPage, gameOverPage, exitPage;

    private CanvasScaler _canvasScaler;

    private bool _isStarted;
    private void Start()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        StartCoroutine(ZoomAnimationIn());
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
        while (_canvasScaler.scaleFactor < 1.06f)
        {
            _canvasScaler.scaleFactor += 0.0017f;
            yield return null;
        }
    }
    
    private IEnumerator ZoomAnimationOut()
    {
        while (_canvasScaler.scaleFactor > 1f)
        {
            _canvasScaler.scaleFactor -= 0.0017f;
            yield return null;
        }
    }
}