using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine.Audio;

public class UIManager : Singleton<UIManager>
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text masterVolumeText = null;
    [SerializeField] private TMP_Text soundVolumeText = null;
    [SerializeField] private TMP_Text musikVolumeText = null;
    [SerializeField] private TMP_Text effectVolumeText = null;
    [SerializeField] private TMP_Text environmentVolumeText = null;

    [SerializeField] private Slider masterVolumeSlider = null;
    [SerializeField] private Slider soundVolumeSlider = null;
    [SerializeField] private Slider musikVolumeSlider = null;
    [SerializeField] private Slider effektVolumeSlider = null;
    [SerializeField] private Slider environmentVolumeSlider = null;

    [SerializeField] private float defaultVolume = 0.5f;

    [SerializeField] private AudioMixer MasterMixer;

    private float masterVolumeValue = 0.5f;
    private float soundVolumeValue = 0.5f;
    private float musikVolumeValue = 0.5f;
    private float effectVolumeValue = 0.5f;
    private float environmentVolumeValue = 0.5f;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text sensitivityYSliderValue = null;
    [SerializeField] private TMP_Text sensitivityXSliderValue = null;

    [SerializeField] private Slider sensitivityYSlider = null;
    [SerializeField] private Slider sensitivityXSlider = null;

    [SerializeField] private float defaultSens = 50;

    private float sensY = 50;
    private float sensX = 50;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 1.0f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private int defaultQualityLevel = 1;

    private int qualityLevel;
    private bool isFullScreen = true;
    private float brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private TextMeshProUGUI confirmationDialog = null;

    [Header("Level to Load")]
    [SerializeField] private string newGameLevel;
    [SerializeField] private string levelToLoad;

    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdown")]
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadPreferences();

        GetAllResolutions();
    }

    //Volume Settings
    public void SetMasterVolume(float volume)
    {
        masterVolumeValue = volume;
        masterVolumeText.text = volume.ToString("0.0");
    }
    public void SetSoundVolume(float volume)
    {
        soundVolumeValue = volume;
        soundVolumeText.text = volume.ToString("0.0");
    }
    public void SetMusikVolume(float volume)
    {
        musikVolumeValue = volume;
        musikVolumeText.text = volume.ToString("0.0");
    }
    public void SetEffectVolume(float volume)
    {
        effectVolumeValue = volume;
        effectVolumeText.text = volume.ToString("0.0");
    }
    public void SetEnvironmentVolume(float volume)
    {
        environmentVolumeValue = volume;
        environmentVolumeText.text = volume.ToString("0.0");
    }
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeValue);
        PlayerPrefs.SetFloat("soundVolume", soundVolumeValue);
        PlayerPrefs.SetFloat("musikVolume", musikVolumeValue);
        PlayerPrefs.SetFloat("effectVolume", effectVolumeValue);
        PlayerPrefs.SetFloat("environmentVolume", environmentVolumeValue);

        MasterMixer.SetFloat("Master", Mathf.Log10(masterVolumeValue) * 20);
        MasterMixer.SetFloat("Sounds", Mathf.Log10(soundVolumeValue) * 20);
        MasterMixer.SetFloat("Music", Mathf.Log10(musikVolumeValue) * 20);
        MasterMixer.SetFloat("SFX", Mathf.Log10(effectVolumeValue) * 20);
        MasterMixer.SetFloat("Environment", Mathf.Log10(environmentVolumeValue) * 20);

        StartCoroutine(ConfirmationWindow("Audiosettings saved", .5f));
    }

    //Gameplay Settings
    public void SetSensitivityY(float sensitivity)
    {
        sensY = Mathf.RoundToInt(sensitivity);
        sensitivityYSliderValue.text = sensitivity.ToString("0");
    }
    public void SetSensitivityX(float sensitivity)
    {
        sensX = Mathf.RoundToInt(sensitivity);
        sensitivityXSliderValue.text = sensitivity.ToString("0");
    }
    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("sensX", sensX);
        PlayerPrefs.SetFloat("sensY", sensY);
        Actions.OnSensitivityChanged(new Vector2(sensX, sensY));
        StartCoroutine(ConfirmationWindow("Gameplaysettings saved", .5f));
    }

    //Graphics Settings
    private void GetAllResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        this.isFullScreen = isFullscreen;
    }
    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }
    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }
    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("brightness", brightnessLevel);

        PlayerPrefs.SetInt("quality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("fullscreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ConfirmationWindow("Graphicsettings saved", 0.5f));
    }

    //UI Button Methods
    public void OnNewGameDialogYes()
    {
        TransitionManager.Instance.TransitionToScene(1, TransitionMethod.LoadingScreen);
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

    //Others
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityLevel = defaultQualityLevel;
            qualityDropdown.value = defaultQualityLevel;
            GraphicsApply();
        }

        if (MenuType == "Audio")
        {
            masterVolumeValue = defaultVolume;
            soundVolumeValue = defaultVolume;
            musikVolumeValue = defaultVolume;
            effectVolumeValue = defaultVolume;
            environmentVolumeValue = defaultVolume;

            masterVolumeSlider.value = defaultVolume;
            soundVolumeSlider.value = defaultVolume;
            musikVolumeSlider.value = defaultVolume;
            effektVolumeSlider.value = defaultVolume;
            environmentVolumeSlider.value = defaultVolume;

            masterVolumeText.text = defaultVolume.ToString("0");
            soundVolumeText.text = defaultVolume.ToString("0");
            musikVolumeText.text = defaultVolume.ToString("0");
            effectVolumeText.text = defaultVolume.ToString("0");
            environmentVolumeText.text = defaultVolume.ToString("0");

            VolumeApply();
        }

        if(MenuType == "Gameplay")
        {
            sensitivityYSliderValue.text = defaultSens.ToString("0");
            sensitivityYSlider.value = defaultSens;

            sensitivityXSliderValue.text = defaultSens.ToString("0");
            sensitivityXSlider.value = defaultSens;

            sensY = defaultSens;
            sensX = defaultSens;
            GameplayApply();
        }
    }

    //Coroutines
    public IEnumerator ConfirmationWindow(string text, float time)
    {
        confirmationDialog.alpha = 1;
        confirmationDialog.GetComponent<TextMeshProUGUI>().text = text;
        confirmationDialog.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationDialog.DOFade(0, time);
        yield return new WaitWhile(() => confirmationDialog.alpha > 0);
        confirmationDialog.gameObject.SetActive(false);
    }

    //Load Preferences
    public void LoadPreferences()
    {
        LoadAudioPreferences();
        LoadGameplayPreferences();
        LoadGraphicsPreferences();
    }

    private void LoadAudioPreferences()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolumeValue = PlayerPrefs.GetFloat("masterVolume");
            soundVolumeValue = PlayerPrefs.GetFloat("soundVolume");
            musikVolumeValue = PlayerPrefs.GetFloat("musikVolume");
            effectVolumeValue = PlayerPrefs.GetFloat("effectVolume");
            environmentVolumeValue = PlayerPrefs.GetFloat("environmentVolume");

            masterVolumeText.text = masterVolumeValue.ToString("0.0");
            soundVolumeText.text = soundVolumeValue.ToString("0.0");
            musikVolumeText.text = musikVolumeValue.ToString("0.0");
            effectVolumeText.text = effectVolumeValue.ToString("0.0");
            environmentVolumeText.text = environmentVolumeValue.ToString("0.0");

            masterVolumeSlider.value = masterVolumeValue;
            soundVolumeSlider.value = soundVolumeValue;
            musikVolumeSlider.value = musikVolumeValue;
            effektVolumeSlider.value = effectVolumeValue;
            environmentVolumeSlider.value = environmentVolumeValue;

            MasterMixer.SetFloat("Master", Mathf.Log10(masterVolumeValue) * 20);
            MasterMixer.SetFloat("Sounds", Mathf.Log10(soundVolumeValue) * 20);
            MasterMixer.SetFloat("Music", Mathf.Log10(musikVolumeValue) * 20);
            MasterMixer.SetFloat("SFX", Mathf.Log10(effectVolumeValue) * 20);
            MasterMixer.SetFloat("Environment", Mathf.Log10(environmentVolumeValue) * 20);
        }
        else
        {
            ResetButton("Audio");
        }
    }
    private void LoadGameplayPreferences()
    {
        if (PlayerPrefs.HasKey("sensY"))
        {
            sensY = PlayerPrefs.GetFloat("sensY");
            sensX = PlayerPrefs.GetFloat("sensX");

            sensitivityYSliderValue.text = sensY.ToString("0.0");
            sensitivityXSliderValue.text = sensX.ToString("0.0");

            sensitivityYSlider.value = sensY;
            sensitivityXSlider.value = sensX;
        }
        else
        {
            ResetButton("Gameplay");
        }
    }
    private void LoadGraphicsPreferences()
    {
        if (PlayerPrefs.HasKey("brightness"))
        {
            brightnessLevel = PlayerPrefs.GetFloat("brightness");
            qualityLevel = PlayerPrefs.GetInt("quality");
            QualitySettings.SetQualityLevel(qualityLevel);

            brightnessTextValue.text = brightnessLevel.ToString("0.0");
            qualityDropdown.value = qualityLevel;
            brightnessSlider.value = brightnessLevel;
        }
        else
        {
            ResetButton("Graphics");
        }
    }

    public void CreateAchivements(Achievement achievement)
    {
        
    }
}
