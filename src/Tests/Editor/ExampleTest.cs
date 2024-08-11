using NUnit.Framework;
using UnityEngine;

public class ExampleTest
{
    [Test]
    public void MathHelper_Lerp_CorrectlyInterpolates()
    {
        // Arrange
        float start = 0f;
        float end = 10f;
        float t = 0.5f;

        // Act
        float result = MathHelper.Lerp(start, end, t);

        // Assert
        Assert.AreEqual(5f, result);
    }

    [Test]
    public void ColorUtility_BlendsColors_Correctly()
    {
        // Arrange
        Color color1 = Color.red;
        Color color2 = Color.blue;
        float blendFactor = 0.5f;

        // Act
        Color result = ColorUtility.BlendColors(color1, color2, blendFactor);

        // Assert
        Assert.AreEqual(new Color(0.5f, 0f, 0.5f), result);
    }
}