using System.Linq;
using UnityEngine;
using UnityEditor;

namespace AULib.Editor
{

    [CustomEditor(typeof(SoundManager))]
    public class SoundManagerInspector : UnityEditor.Editor
    {
        SoundManager soundManager;
        string filePath = "Assets/AULib/Scripts/Enums/";
        string fileName = "SoundEffectEnum";

        private void OnEnable()
        {
            soundManager = (SoundManager)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(true);
            filePath = EditorGUILayout.TextField("Path", filePath);
            fileName = EditorGUILayout.TextField("Name", fileName);
            EditorGUI.EndDisabledGroup();


            if (GUILayout.Button("Save"))
            {
                EdiorMethods.WriteToEnum(filePath, fileName, soundManager.EffectSounds.Select(effect => effect.name).ToList());
            }
        }
    }
}
