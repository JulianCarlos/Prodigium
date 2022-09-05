using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    public string MapName { get; private set; } = "Test1";
    public int MapSceneIndex { get; private set; } = 3;
    public GameObject ButtonObject;

    private void OnEnable()
    {
        GetComponent<LayoutElement>().minWidth = 0f;
    }
}
