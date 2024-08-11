using UnityEngine;

public class DebugTools : MonoBehaviour
{
    public static DebugTools instance;

    [Header("Debug Settings")]
    public bool enableDebugLogs = true;
    public bool enableDebugGizmos = true;
    public Color gizmoColor = Color.green;
    public float gizmoDuration = 2.0f;

    private void Awake()
    {
        // Ensure there's only one instance of DebugTools
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
    /// Logs a message to the console if debugging is enabled.
    /// </summary>
    public void Log(string message)
    {
        if (enableDebugLogs)
        {
            Debug.Log($"[DebugTools]: {message}");
        }
    }

    /// <summary>
    /// Logs a warning to the console if debugging is enabled.
    /// </summary>
    public void LogWarning(string message)
    {
        if (enableDebugLogs)
        {
            Debug.LogWarning($"[DebugTools]: {message}");
        }
    }

    /// <summary>
    /// Logs an error to the console if debugging is enabled.
    /// </summary>
    public void LogError(string message)
    {
        if (enableDebugLogs)
        {
            Debug.LogError($"[DebugTools]: {message}");
        }
    }

    /// <summary>
    /// Draws a line in the scene view if gizmos are enabled.
    /// </summary>
    public void DrawLine(Vector3 start, Vector3 end, Color? color = null)
    {
        if (enableDebugGizmos)
        {
            Debug.DrawLine(start, end, color ?? gizmoColor, gizmoDuration);
        }
    }

    /// <summary>
    /// Draws a ray in the scene view if gizmos are enabled.
    /// </summary>
    public void DrawRay(Vector3 start, Vector3 direction, Color? color = null)
    {
        if (enableDebugGizmos)
        {
            Debug.DrawRay(start, direction, color ?? gizmoColor, gizmoDuration);
        }
    }

    /// <summary>
    /// Draws a sphere in the scene view if gizmos are enabled.
    /// </summary>
    public void DrawSphere(Vector3 position, float radius, Color? color = null)
    {
        if (enableDebugGizmos)
        {
            Gizmos.color = color ?? gizmoColor;
            Gizmos.DrawSphere(position, radius);
        }
    }

    /// <summary>
    /// Draws a wireframe cube in the scene view if gizmos are enabled.
    /// </summary>
    public void DrawWireCube(Vector3 position, Vector3 size, Color? color = null)
    {
        if (enableDebugGizmos)
        {
            Gizmos.color = color ?? gizmoColor;
            Gizmos.DrawWireCube(position, size);
        }
    }

    private void OnDrawGizmos()
    {
        // Example usage for drawing a gizmo in the scene view (you can remove this or expand as needed)
        if (enableDebugGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }
}