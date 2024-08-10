using UnityEditor;
using UnityEngine;

public class CustomMenuItems
{
    [MenuItem("Tools/Reset Player Prefs")]
    private static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Player Prefs reset.");
    }

    [MenuItem("Tools/Clear Console %#c")] // CTRL+SHIFT+C
    private static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    [MenuItem("Tools/Toggle Fullscreen _F11")] // F11 for fullscreen toggle
    private static void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen mode toggled.");
    }
}