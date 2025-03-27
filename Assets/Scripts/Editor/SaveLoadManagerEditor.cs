using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SaveLoadManager))]
    public class SaveLoadManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Load Game"))
            {
                (serializedObject.targetObject as SaveLoadManager)?.LoadGame();
            }

            if (GUILayout.Button("Save Game"))
            {
                (serializedObject.targetObject as SaveLoadManager)?.SaveGame();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
