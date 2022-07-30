using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressSliderValue;

    private void Start()
    {
        StartCoroutine(LoadAsync(TransitionManager.TargetIndex));
    }

    private IEnumerator LoadAsync (int sceneIndex)
    {
        yield return new WaitForSeconds(3);

        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneIndex);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            float progress = Mathf.Clamp01(loadAsync.progress / 9f);
            Debug.Log(progress);
            progressSliderValue.text = progress * 100f + "%";
            progressSlider.value = progress;
            Debug.Log("still loading");
            yield return null;

            if (loadAsync.progress >= 0.9f)
            {
                TransitionManager.Instance.FadeOut();
                yield return new WaitForSeconds(3);
                loadAsync.allowSceneActivation = true;
                Debug.Log(loadAsync.progress);
            }
        }
        yield return null;
    }
}
