using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController instance;

    [Header("References")]
    public AudioManager audioManager;
    public AudioVisualizer audioVisualizer;

    [Header("Settings")]
    public bool startOnAwake = true;
    public string nextSceneName;

    private void Awake()
    {
        // Ensure there's only one instance of MainController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize systems
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (audioVisualizer == null)
        {
            audioVisualizer = FindObjectOfType<AudioVisualizer>();
        }

        if (startOnAwake)
        {
            StartVisualization();
        }
    }

    public void StartVisualization()
    {
        if (audioManager != null)
        {
            audioManager.PlayAudio();
        }

        if (audioVisualizer != null)
        {
            audioVisualizer.enabled = true; // Enable the visualizer
        }
    }

    public void StopVisualization()
    {
        if (audioManager != null)
        {
            audioManager.StopAudio();
        }

        if (audioVisualizer != null)
        {
            audioVisualizer.enabled = false; // Disable the visualizer
        }
    }

    public void RestartVisualization()
    {
        StopVisualization();
        StartVisualization();
    }

    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }

    // public void LoadNextScene()
    // {
    //     SceneLoader.instance.LoadNextScene();
    // }
    public void QuitApplication()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
    }

    private void Update()
    {
        // Example of handling input for scene management or visualization control
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartVisualization();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitApplication();
        }
    }
}