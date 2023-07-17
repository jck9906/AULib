using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEditor;

namespace AULib.Editor
{

    [CustomEditor(typeof(AutoRotate))]
    public class AutoRotateInspector : UnityEditor.Editor
    {

        AutoRotate autoRotate;


        private void OnEnable()
        {
            autoRotate = (AutoRotate)target;
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (autoRotate.IsYoYo)
            {
                autoRotate.DefaultAngle = EditorGUILayout.Vector3Field("DefaultAngle", autoRotate.DefaultAngle);
            }



            serializedObject.ApplyModifiedProperties();
        }
    }
}
