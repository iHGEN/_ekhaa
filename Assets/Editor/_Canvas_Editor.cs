using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(_Canvas))]
public class _Canvas_Editor : Editor
{
    int number = 0;
    string text;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (number == 0)
        {
            text = "No Canvas Found";
        }
        else
        {
            text = $"Total Canvas : {number}";
        }

        EditorGUILayout.LabelField(text);

        _Canvas canvasScript = (_Canvas)target;

        if (GUILayout.Button("Find All Canvas"))
        {
            number = canvasScript.find_all_canvas();
        }

        if (GUILayout.Button("Add"))
        {
            if (number != 0)
            {
                canvasScript.target();
            }
        }
    }
}
