using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    public Transform[] visualizerBars;
    public float scaleMultiplier = 10.0f;
    public int sampleSize = 512;

    private AudioSource audioSource;
    private float[] audioSamples;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSamples = new float[sampleSize];
    }

    void Update()
    {
        audioSource.GetSpectrumData(audioSamples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < visualizerBars.Length; i++)
        {
            float scale = Mathf.Clamp(audioSamples[i] * scaleMultiplier, 0.1f, 10.0f);
            visualizerBars[i].localScale = new Vector3(1, scale, 1);
        }
    }
}