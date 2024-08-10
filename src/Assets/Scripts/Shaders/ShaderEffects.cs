using UnityEngine;

public class ShaderEffects : MonoBehaviour
{
    public static ShaderEffects instance;

    private void Awake()
    {
        // Ensure there's only one instance of ShaderEffects
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
    /// Changes the color of the material.
    /// </summary>
    /// <param name="material">The material to change the color of.</param>
    /// <param name="color">The new color.</param>
    public void ChangeColor(Material material, Color color)
    {
        if (material.HasProperty("_Color"))
        {
            material.SetColor("_Color", color);
        }
    }

    /// <summary>
    /// Applies a gradient effect to a material by interpolating between two colors.
    /// </summary>
    /// <param name="material">The material to apply the gradient to.</param>
    /// <param name="color1">The start color of the gradient.</param>
    /// <param name="color2">The end color of the gradient.</param>
    /// <param name="blendFactor">A value between 0 and 1 that determines the blend between the two colors.</param>
    public void ApplyGradient(Material material, Color color1, Color color2, float blendFactor)
    {
        if (material.HasProperty("_Color1") && material.HasProperty("_Color2") && material.HasProperty("_BlendFactor"))
        {
            material.SetColor("_Color1", color1);
            material.SetColor("_Color2", color2);
            material.SetFloat("_BlendFactor", Mathf.Clamp01(blendFactor));
        }
    }

    /// <summary>
    /// Adjusts the tiling of the texture on the material.
    /// </summary>
    /// <param name="material">The material to adjust the tiling of.</param>
    /// <param name="tilingX">The tiling value in the X direction.</param>
    /// <param name="tilingY">The tiling value in the Y direction.</param>
    public void AdjustTiling(Material material, float tilingX, float tilingY)
    {
        if (material.HasProperty("_MainTex"))
        {
            material.SetTextureScale("_MainTex", new Vector2(tilingX, tilingY));
        }
    }

    /// <summary>
    /// Adjusts the offset of the texture on the material.
    /// </summary>
    /// <param name="material">The material to adjust the offset of.</param>
    /// <param name="offsetX">The offset value in the X direction.</param>
    /// <param name="offsetY">The offset value in the Y direction.</param>
    public void AdjustOffset(Material material, float offsetX, float offsetY)
    {
        if (material.HasProperty("_MainTex"))
        {
            material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        }
    }

    /// <summary>
    /// Animates the UV coordinates of a material's texture.
    /// </summary>
    /// <param name="material">The material to animate the UV coordinates of.</param>
    /// <param name="speedX">The speed of movement in the X direction.</param>
    /// <param name="speedY">The speed of movement in the Y direction.</param>
    public void AnimateUV(Material material, float speedX, float speedY)
    {
        if (material.HasProperty("_MainTex"))
        {
            Vector2 offset = material.GetTextureOffset("_MainTex");
            offset.x += speedX * Time.deltaTime;
            offset.y += speedY * Time.deltaTime;
            material.SetTextureOffset("_MainTex", offset);
        }
    }

    /// <summary>
    /// Pulses the emission intensity of a material.
    /// </summary>
    /// <param name="material">The material to pulse the emission of.</param>
    /// <param name="minEmission">The minimum emission intensity.</param>
    /// <param name="maxEmission">The maximum emission intensity.</param>
    /// <param name="speed">The speed of the pulsing effect.</param>
    public void PulseEmission(Material material, float minEmission, float maxEmission, float speed)
    {
        if (material.HasProperty("_EmissionColor"))
        {
            float emission = Mathf.PingPong(Time.time * speed, maxEmission - minEmission) + minEmission;
            Color baseColor = material.GetColor("_EmissionColor");
            material.SetColor("_EmissionColor", baseColor * Mathf.LinearToGammaSpace(emission));
        }
    }

    /// <summary>
    /// Rotates the UV coordinates of a material's texture.
    /// </summary>
    /// <param name="material">The material to rotate the UV coordinates of.</param>
    /// <param name="angle">The angle of rotation in degrees.</param>
    public void RotateUV(Material material, float angle)
    {
        if (material.HasProperty("_MainTex"))
        {
            // Calculate rotation matrix
            float rad = angle * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);

            Matrix4x4 rotationMatrix = Matrix4x4.identity;
            rotationMatrix.m00 = cos;
            rotationMatrix.m01 = -sin;
            rotationMatrix.m10 = sin;
            rotationMatrix.m11 = cos;

            material.SetMatrix("_RotationMatrix", rotationMatrix);
        }
    }

    /// <summary>
    /// Sets the float value of a shader property.
    /// </summary>
    /// <param name="material">The material to set the property on.</param>
    /// <param name="propertyName">The name of the float property.</param>
    /// <param name="value">The value to set.</param>
    public void SetShaderFloat(Material material, string propertyName, float value)
    {
        if (material.HasProperty(propertyName))
        {
            material.SetFloat(propertyName, value);
        }
    }

    /// <summary>
    /// Sets the vector value of a shader property.
    /// </summary>
    /// <param name="material">The material to set the property on.</param>
    /// <param name="propertyName">The name of the vector property.</param>
    /// <param name="value">The vector value to set.</param>
    public void SetShaderVector(Material material, string propertyName, Vector4 value)
    {
        if (material.HasProperty(propertyName))
        {
            material.SetVector(propertyName, value);
        }
    }
}