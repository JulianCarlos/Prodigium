using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : EventTrigger
{
    private Vector3 startingScale;

    private void Awake()
    {
        startingScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = startingScale;
    }

    public void OnButtonHoverEnter()
    {
        transform.localScale = startingScale * 1.1f;
    }

    public void OnButtonHoverExit()
    {
        transform.localScale = startingScale;
    }
}
