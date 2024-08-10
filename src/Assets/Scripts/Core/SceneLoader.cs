using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [Header("Loading Settings")]
    public bool showLoadingScreen = true;
    public GameObject loadingScreen;
    public UnityEngine.UI.Slider progressBar;

    private void Awake()
    {
        // Ensure there's only one instance of SceneLoader
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false); // Hide the loading screen initially
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        LoadScene(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name);
    }

    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = (currentSceneIndex - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
        LoadScene(SceneManager.GetSceneByBuildIndex(previousSceneIndex).name);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (showLoadingScreen && loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Update progress bar if available
            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            }

            // Check if the scene is ready to be activated
            if (asyncOperation.progress >= 0.9f)
            {
                if (showLoadingScreen && loadingScreen != null)
                {
                    loadingScreen.SetActive(false); // Hide loading screen when done
                }
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}