using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Level to Load")]
    [SerializeField] private string newGameLevel;
    [SerializeField] private string levelToLoad;

    [SerializeField] private GameObject noSavedGameDialog = null;

    public void OnNewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void OnLoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
