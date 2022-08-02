using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public enum TransitionMethod
{
    LoadingScreen,
    BlackScreen
}
public class TransitionManager : Singleton<TransitionManager>
{
    public static int TargetIndex;

    [SerializeField] private Image whiteScreen;
    [SerializeField] private Image blackSreen;

    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void Start()
    {
        FadeIn(3);
    }

    //Fade Methods
    public void FadeIn()
    {
        Actions.OnFadeInBegin(this);
        blackSreen.raycastTarget = false;
        blackSreen.DOFade(0, fadeInTime).From(1);
    }
    public void FadeIn(float time)
    {
        Actions.OnFadeInBegin(this);
        blackSreen.raycastTarget = false;
        blackSreen.DOFade(0, time).From(1);
    }
    public void FadeOut()
    {
        Actions.OnFadeOutBegin(this);
        blackSreen.raycastTarget = true;
        blackSreen.DOFade(1, fadeOutTime).From(0);
    }
    public void FadeOut(float time)
    {
        Actions.OnFadeOutBegin(this);
        blackSreen.raycastTarget = true;
        blackSreen.DOFade(1, time).From(0);
    }

    //Scene Transitioning
    public void TransitionToScene(int sceneIndex, TransitionMethod method)
    {
        if(method == TransitionMethod.LoadingScreen)
        {
            StartCoroutine(LoadingToNewLevel(sceneIndex));
        }
        else if(method == TransitionMethod.BlackScreen)
        {
            StartCoroutine(ChangeToNewLevel(sceneIndex));
        }
    }

    //Coroutines
    private IEnumerator LoadingToNewLevel(int sceneIndex)
    {
        TargetIndex = sceneIndex;
        FadeOut();
        yield return new WaitForSeconds(fadeOutTime);
        SceneManager.LoadScene(2);
    }
    private IEnumerator ChangeToNewLevel(int sceneIndex)
    {
        FadeOut();
        yield return new WaitForSeconds(fadeOutTime);
        SceneManager.LoadScene(sceneIndex);
    }

    //OnScene Changed 
    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }
}
