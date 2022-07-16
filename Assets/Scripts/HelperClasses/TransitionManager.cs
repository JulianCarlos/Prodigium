using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private Image blackSreen;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void FadeIn(float fadeTime)
    {
        blackSreen.DOFade(0, fadeTime);
    }

    public void FadeOut(float fadeTime)
    {
        blackSreen.DOFade(1, fadeTime);
    }
}
