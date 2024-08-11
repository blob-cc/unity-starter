using UnityEditor;
using UnityEngine;

public class AssetRenamer : EditorWindow
{
    private string searchTerm = "Old";
    private string replaceTerm = "New";

    [MenuItem("Tools/Asset Renamer")]
    public static void ShowWindow()
    {
        GetWindow<AssetRenamer>("Asset Renamer");
    }

    void OnGUI()
    {
        GUILayout.Label("Rename Assets", EditorStyles.boldLabel);
        searchTerm = EditorGUILayout.TextField("Search Term", searchTerm);
        replaceTerm = EditorGUILayout.TextField("Replace Term", replaceTerm);

        if (GUILayout.Button("Rename"))
        {
            RenameAssets();
        }
    }

    private void RenameAssets()
    {
        string[] guids = AssetDatabase.FindAssets(searchTerm);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string newName = path.Replace(searchTerm, replaceTerm);
            AssetDatabase.RenameAsset(path, newName);
        }
        AssetDatabase.SaveAssets();
    }
}