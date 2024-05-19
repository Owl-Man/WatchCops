using System.Collections;
using UnityEngine;

public class LoadingMenu : MonoBehaviour
{
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);
        _animator.Play("loadingMenu");
    }
}
