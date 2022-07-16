using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionManager : Singleton<TransitionManager>
{
    public void FadeIn(Image targetImage, float fadeTime)
    {
        targetImage.DOFade(0, fadeTime);
    }

    public void FadeOut(Image targetImage, float fadeTime)
    {
        targetImage.DOFade(1, fadeTime);
    }
}
