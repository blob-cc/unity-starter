using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public static NoiseGenerator instance;

    private void Awake()
    {
        // Ensure there's only one instance of NoiseGenerator
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

    /// <summary>
    /// Generates a 2D Perlin noise value.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>Perlin noise value between 0 and 1.</returns>
    public float GeneratePerlinNoise(float x, float y, float scale)
    {
        float perlinValue = Mathf.PerlinNoise(x * scale, y * scale);
        return perlinValue;
    }

    /// <summary>
    /// Generates a 2D Simplex noise value.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>Simplex noise value between -1 and 1.</returns>
    public float GenerateSimplexNoise(float x, float y, float scale)
    {
        return SimplexNoise.Noise(x * scale, y * scale);
    }

    /// <summary>
    /// Generates a 3D Perlin noise value.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="z">Z coordinate.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>Perlin noise value between 0 and 1.</returns>
    public float GeneratePerlinNoise3D(float x, float y, float z, float scale)
    {
        float xy = Mathf.PerlinNoise(x * scale, y * scale);
        float yz = Mathf.PerlinNoise(y * scale, z * scale);
        float zx = Mathf.PerlinNoise(z * scale, x * scale);

        float perlinValue = (xy + yz + zx) / 3f;
        return perlinValue;
    }

    /// <summary>
    /// Generates a 3D Simplex noise value.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="z">Z coordinate.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>Simplex noise value between -1 and 1.</returns>
    public float GenerateSimplexNoise3D(float x, float y, float z, float scale)
    {
        return SimplexNoise.Noise(x * scale, y * scale, z * scale);
    }

    /// <summary>
    /// Generates a 2D texture based on Perlin noise.
    /// </summary>
    /// <param name="width">Width of the texture.</param>
    /// <param name="height">Height of the texture.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>A texture filled with Perlin noise.</returns>
    public Texture2D GeneratePerlinNoiseTexture(int width, int height, float scale)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sample = GeneratePerlinNoise(x, y, scale);
                pixels[y * width + x] = new Color(sample, sample, sample);
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }

    /// <summary>
    /// Generates a 2D texture based on Simplex noise.
    /// </summary>
    /// <param name="width">Width of the texture.</param>
    /// <param name="height">Height of the texture.</param>
    /// <param name="scale">Scale of the noise.</param>
    /// <returns>A texture filled with Simplex noise.</returns>
    public Texture2D GenerateSimplexNoiseTexture(int width, int height, float scale)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sample = GenerateSimplexNoise(x, y, scale);
                sample = Mathf.InverseLerp(-1f, 1f, sample); // Normalize to 0-1 range
                pixels[y * width + x] = new Color(sample, sample, sample);
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}

public static class SimplexNoise
{
    // Implement the Simplex noise algorithm or use an external library.
    // This is a placeholder for the Simplex noise function.
    public static float Noise(float x, float y)
    {
        // Placeholder implementation. Replace with actual Simplex noise algorithm.
        return Mathf.PerlinNoise(x, y) * 2f - 1f;
    }

    public static float Noise(float x, float y, float z)
    {
        // Placeholder implementation. Replace with actual 3D Simplex noise algorithm.
        return Mathf.PerlinNoise(x, y) * 2f - 1f;
    }
}