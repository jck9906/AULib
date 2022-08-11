using System.Linq;
using UnityEngine;
using UnityEditor;

namespace AULib.Editor
{

    
    [CustomEditor(typeof(ToggleEx))]
    public class ToggleExInspector : UnityEditor.Editor
    {

        SerializedProperty toggleType;
        SerializedProperty graphics;
        SerializedProperty colorDefault;
        SerializedProperty colorSelect;
        SerializedProperty actvieToggle;
        SerializedProperty deActvieToggle;

        ToggleEx toggleEx;
        
        private void OnEnable()
        {
            toggleEx = (ToggleEx)target;
            toggleType = serializedObject.FindProperty("toggleType");
            graphics = serializedObject.FindProperty("graphics");
            colorDefault = serializedObject.FindProperty("colorDefault");
            colorSelect = serializedObject.FindProperty("colorSelect");
            actvieToggle = serializedObject.FindProperty("actvieToggle");
            deActvieToggle = serializedObject.FindProperty("deActvieToggle");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(toggleType);
            switch (toggleEx.toggleType)
            {
                case ToggleEx.eToggleType.ColorChange:
                    EditorGUILayout.PropertyField(graphics);
                    EditorGUILayout.PropertyField(colorDefault);
                    EditorGUILayout.PropertyField(colorSelect);
                    break;

                case ToggleEx.eToggleType.SwapGameObject:
                    EditorGUILayout.PropertyField(actvieToggle);
                    EditorGUILayout.PropertyField(deActvieToggle);
                    break;

                default:
                    break;
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
