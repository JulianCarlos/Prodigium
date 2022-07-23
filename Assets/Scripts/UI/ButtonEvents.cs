using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private float sizeHoverMultiplier = 1.2f;

    private Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
    }

    public void OnHoverEnter()
    {
        transform.DOScale(localScale * sizeHoverMultiplier, 1);
    }

    public void OnHoverExit()
    {
        transform.DOScale(localScale, 1);
    }
}
