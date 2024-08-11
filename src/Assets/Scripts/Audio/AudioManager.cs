using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source Settings")]
    public AudioSource audioSource;

    [Header("Audio Control")]
    public bool playOnStart = true;
    public bool loopAudio = true;
    public float volume = 1.0f;

    void Awake()
    {
        // Ensure there's only one instance of AudioManager
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

    void Start()
    {
        // Set initial audio source settings
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.loop = loopAudio;
        audioSource.volume = volume;

        if (playOnStart)
        {
            PlayAudio();
        }
    }

    // Play the audio
    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Pause the audio
    public void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Stop the audio
    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Adjust the audio volume
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0.0f, 1.0f);
        audioSource.volume = volume;
    }

    // Check if the audio is playing
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}