using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup dialog;

    private void OnEnable()
    {
        var sequence = DOTween.Sequence();
        sequence
            .Append(
            dialog.DOFade(1, 0.5f).From(0.2f))
            .Play();
    }
}
