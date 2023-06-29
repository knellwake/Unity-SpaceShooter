using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    public static FadeCanvas fader;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted = false;

    private void Awake()
    {
        if (fader == null)
        {
            fader = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOutString(levelName));
    }

    public void FaderLoadInt(int levelIndex)
    {
        StartCoroutine(FadeOutInt(levelIndex));
    }
    
    /// <summary>
    /// 淡入-黑屏变透明(1 -> 0)
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        loadingScreen.SetActive(false);
        fadeStarted = false;
        while (canvasGroup.alpha > 0)
        {
            //直接中断淡入 ， 切换至淡出加载进入关卡
            if (fadeStarted)
                yield break;
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }
    }

    /// <summary>
    /// 透明变黑（0 -> 1）
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    IEnumerator FadeOutString(string levelName)
    {
        // if(canvasGroup.alpha !=0 )
        //     yield break;
        if(fadeStarted)
            yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        // SceneManager.LoadScene(levelName);
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);
        ao.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
        
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutInt(int levelIndex)
    {
        if (fadeStarted)
            yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        // SceneManager.LoadScene(levelIndex);
        // yield return new WaitForSeconds(0.1f);

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelIndex);
        ao.allowSceneActivation = false;
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }
}
