using UnityEngine;
using UnityEditor;

namespace AULib.Editor
{
    [CustomEditor(typeof(FontSetter<>), true)]
    public class FontSetterInspector : UnityEditor.Editor
    {

        SerializedProperty _isSetFontWithFontManager;
        SerializedProperty _fontType;
        SerializedProperty _textField;

        IFontSetter fontSetter;

        private void OnEnable()
        {
            fontSetter = (IFontSetter)target;
            _isSetFontWithFontManager = serializedObject.FindProperty("_isSetFontWithFontManager");
            _fontType = serializedObject.FindProperty("_fontType");
            _textField = serializedObject.FindProperty("_textField");        
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_isSetFontWithFontManager);
            

            if (fontSetter.GetIsSetFontWithFontManager())
            {
                EditorGUILayout.PropertyField(_textField);
                EditorGUILayout.PropertyField(_fontType);

                EditorGUILayout.Space();
                if (GUILayout.Button("Set font to TextField"))
                {
                    fontSetter.SetFontEditor();
                }
            }


            

            serializedObject.ApplyModifiedProperties();
        }
    }
}
