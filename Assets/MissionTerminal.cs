using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject missionCanvas;

    [Header("Map Settings")]
    [SerializeField] private MapDisplay currentSelectedMap;
    [SerializeField] private MapDisplay[] maps;

    [Header("Chosen Map Settings")]
    [SerializeField] private MapDisplay chosenMap;
    [SerializeField] private string chosenMapName;
    [SerializeField] private int chosenMapSceneIndex;

    [Header("Map Preview Settings")]
    [SerializeField] private float expandAmount;

    private void Awake()
    {
        maps = GetComponentsInChildren<MapDisplay>();
    }

    public void Interact()
    {
        missionCanvas.SetActive(true);
        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    //Button Events
    public void SelectMap(MapDisplay display)
    {
        currentSelectedMap = display;
        currentSelectedMap.GetComponent<LayoutElement>().minWidth = expandAmount;
        currentSelectedMap.ButtonObject?.SetActive(true);
    }
    public void DeSelectMap(MapDisplay display)
    {
        if(currentSelectedMap = display)
        {
            currentSelectedMap.GetComponent<LayoutElement>().minWidth = 0;
            currentSelectedMap.ButtonObject?.SetActive(false);
            currentSelectedMap = null;
        }
    }
    public void ChooseMap(MapDisplay display)
    {
        chosenMap = display;
        chosenMapName = display.MapName;
        chosenMapSceneIndex = display.MapSceneIndex;
    }

    public void JoinMap()
    {
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
        TransitionManager.Instance.TransitionToScene(chosenMapSceneIndex, TransitionMethod.LoadingScreen);
    }
}
