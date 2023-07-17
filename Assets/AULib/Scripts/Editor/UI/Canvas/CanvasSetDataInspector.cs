using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



namespace AULib.Editor
{

    /// <summary>
    /// CanvasSetData 인스펙터 셋팅
    /// </summary>
    [CustomEditor(typeof(CanvasSetData))]
    public class CanvasSetDataInspector : UnityEditor.Editor
    {

        private CanvasSetData _canvasSetData;

        SerializedProperty renderMode;
        SerializedProperty guide;
        SerializedProperty sortOrder;
        SerializedProperty pixelPerfect;
        SerializedProperty targetCamera;
        SerializedProperty sortingLayerID;

        private void OnEnable()
        {
            _canvasSetData = (CanvasSetData)target;

            renderMode = serializedObject.FindProperty("RenderMode");
            guide = serializedObject.FindProperty("Guide");
            sortOrder = serializedObject.FindProperty("SortOrder");
            pixelPerfect = serializedObject.FindProperty("PixelPerfect");
            targetCamera = serializedObject.FindProperty("TargetCamera");
            sortingLayerID = serializedObject.FindProperty("SortingLayerID");
        }

        public override void OnInspectorGUI()
        {


            serializedObject.Update();
            EditorGUILayout.PropertyField(guide);
            EditorGUILayout.PropertyField(renderMode);
            //base.OnInspectorGUI();

            //공통
            //Guide
            //PixelPerfect
            switch (_canvasSetData.RenderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    //Sort Order 
                    EditorGUILayout.PropertyField(pixelPerfect);
                    EditorGUILayout.PropertyField(sortOrder);
                    break;

                case RenderMode.ScreenSpaceCamera:
                    //Target Camera
                    EditorGUILayout.PropertyField(pixelPerfect);
                    //EditorGUILayout.PropertyField(sortingLayer);
                    var layers = SortingLayer.layers;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    int newId = DrawSortingLayersPopup(_canvasSetData.SortingLayerID);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _canvasSetData.SortingLayerID = newId;
                    }
                    EditorGUILayout.EndHorizontal();

                    _canvasSetData.SortOrder = EditorGUILayout.IntField("Order in layer", _canvasSetData.SortOrder);
                    break;

                case RenderMode.WorldSpace:
                    break;
            }




            serializedObject.ApplyModifiedProperties();
        }






        int DrawSortingLayersPopup(int layerID)
        {
            var layers = SortingLayer.layers;
            var names = layers.Select(l => l.name).ToArray();
            if (!SortingLayer.IsValid(layerID))
            {
                layerID = layers[0].id;
            }
            var layerValue = SortingLayer.GetLayerValueFromID(layerID);
            var newLayerValue = EditorGUILayout.Popup("Sorting Layer", layerValue, names);
            return layers[newLayerValue].id;
        }
    }
}
