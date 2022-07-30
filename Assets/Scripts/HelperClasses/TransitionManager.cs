using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    }

    //Fade Methods
    public void FadeIn()
    {
        blackSreen.raycastTarget = false;
        blackSreen.DOFade(0, fadeInTime).From(1);
    }
    public void FadeOut()
    {
        blackSreen.raycastTarget = true;
        blackSreen.DOFade(1, fadeOutTime).From(0);
    }

    //Scene Transitioning
    public void TransitionToScene(int sceneIndex)
    {
        StartCoroutine(ChangeScene(sceneIndex));
    }

    private IEnumerator ChangeScene(int sceneIndex)
    {
        TargetIndex = sceneIndex;
        FadeOut();
        yield return new WaitForSeconds(fadeOutTime);
        SceneManager.LoadScene(2);
    }

    private void OnLevelWasLoaded(int level)
    {
        FadeIn();
    }
}
