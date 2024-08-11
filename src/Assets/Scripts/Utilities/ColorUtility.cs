using UnityEngine;

public static class ColorUtility
{
    /// <summary>
    /// Converts a color from RGB to HSV.
    /// </summary>
    /// <param name="color">The RGB color to convert.</param>
    /// <returns>The HSV representation of the color.</returns>
    public static Vector3 RGBToHSV(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return new Vector3(h, s, v);
    }

    /// <summary>
    /// Converts a color from HSV to RGB.
    /// </summary>
    /// <param name="hsv">The HSV color to convert.</param>
    /// <returns>The RGB representation of the color.</returns>
    public static Color HSVToRGB(Vector3 hsv)
    {
        return Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
    }

    /// <summary>
    /// Generates a random color.
    /// </summary>
    /// <returns>A random color.</returns>
    public static Color RandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    /// <summary>
    /// Generates a random color within a specified range of brightness.
    /// </summary>
    /// <param name="minBrightness">The minimum brightness of the color.</param>
    /// <param name="maxBrightness">The maximum brightness of the color.</param>
    /// <returns>A random color within the specified brightness range.</returns>
    public static Color RandomColor(float minBrightness, float maxBrightness)
    {
        float h = UnityEngine.Random.value;
        float s = UnityEngine.Random.Range(0.5f, 1f);  // Saturation range
        float v = UnityEngine.Random.Range(minBrightness, maxBrightness);  // Brightness range
        return Color.HSVToRGB(h, s, v);
    }

    /// <summary>
    /// Blends two colors together.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="blendFactor">The blend factor (0 = color1, 1 = color2).</param>
    /// <returns>The blended color.</returns>
    public static Color BlendColors(Color color1, Color color2, float blendFactor)
    {
        return Color.Lerp(color1, color2, Mathf.Clamp01(blendFactor));
    }

    /// <summary>
    /// Creates a gradient between two colors.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="steps">The number of steps in the gradient.</param>
    /// <returns>An array of colors representing the gradient.</returns>
    public static Color[] CreateGradient(Color color1, Color color2, int steps)
    {
        Color[] gradient = new Color[steps];

        for (int i = 0; i < steps; i++)
        {
            float t = i / (float)(steps - 1);
            gradient[i] = BlendColors(color1, color2, t);
        }

        return gradient;
    }

    /// <summary>
    /// Adjusts the brightness of a color.
    /// </summary>
    /// <param name="color">The color to adjust.</param>
    /// <param name="brightnessFactor">The brightness factor (1 = no change).</param>
    /// <returns>The color with adjusted brightness.</returns>
    public static Color AdjustBrightness(Color color, float brightnessFactor)
    {
        Vector3 hsv = RGBToHSV(color);
        hsv.z = Mathf.Clamp01(hsv.z * brightnessFactor);
        return HSVToRGB(hsv);
    }

    /// <summary>
    /// Adjusts the saturation of a color.
    /// </summary>
    /// <param name="color">The color to adjust.</param>
    /// <param name="saturationFactor">The saturation factor (1 = no change).</param>
    /// <returns>The color with adjusted saturation.</returns>
    public static Color AdjustSaturation(Color color, float saturationFactor)
    {
        Vector3 hsv = RGBToHSV(color);
        hsv.y = Mathf.Clamp01(hsv.y * saturationFactor);
        return HSVToRGB(hsv);
    }

    /// <summary>
    /// Converts a color from RGB to hexadecimal format.
    /// </summary>
    /// <param name="color">The RGB color to convert.</param>
    /// <returns>The hexadecimal representation of the color.</returns>
    public static string RGBToHex(Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }

    /// <summary>
    /// Converts a hexadecimal color string to an RGB color.
    /// </summary>
    /// <param name="hex">The hexadecimal color string to convert.</param>
    /// <returns>The RGB representation of the color.</returns>
    public static Color HexToRGB(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex string: " + hex);
            return Color.white;
        }
    }

    /// <summary>
    /// Calculates the contrast ratio between two colors.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <returns>The contrast ratio between the two colors.</returns>
    public static float ContrastRatio(Color color1, Color color2)
    {
        float l1 = CalculateLuminance(color1);
        float l2 = CalculateLuminance(color2);

        if (l1 > l2)
        {
            return (l1 + 0.05f) / (l2 + 0.05f);
        }
        else
        {
            return (l2 + 0.05f) / (l1 + 0.05f);
        }
    }

    /// <summary>
    /// Calculates the luminance of a color.
    /// </summary>
    /// <param name="color">The color to calculate the luminance for.</param>
    /// <returns>The luminance value of the color.</returns>
    private static float CalculateLuminance(Color color)
    {
        float r = color.r <= 0.03928f ? color.r / 12.92f : Mathf.Pow((color.r + 0.055f) / 1.055f, 2.4f);
        float g = color.g <= 0.03928f ? color.g / 12.92f : Mathf.Pow((color.g + 0.055f) / 1.055f, 2.4f);
        float b = color.b <= 0.03928f ? color.b / 12.92f : Mathf.Pow((color.b + 0.055f) / 1.055f, 2.4f);

        return 0.2126f * r + 0.7152f * g + 0.0722f * b;
    }
}