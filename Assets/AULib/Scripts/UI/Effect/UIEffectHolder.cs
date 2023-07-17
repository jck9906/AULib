using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using Coffee.UIExtensions;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AULib
{

    public enum eUIAction
    {
        Play,
        Stop,

        EndOfEnum
    }


    // 동시에 시작
    // 동시에 강제 끝
    // 관리 대상 외에는 내 알바 아님....
    // 이벤트 기준 플레이 대상.
    //  -> Enable
    //  -> 
    public class UIEffectHolder : MonoBehaviour
    {
        public List<IUIEffect> _effectList = new List<IUIEffect>();

        // Dictionary<int , List<GameObject>> _effectMap; //  = new Dictionary<int , List<GameObject>>();
        // UI Effect
        // Effect Factor : 0 ~ 1 변경

        private void OnEnable()
        {
            foreach( var effect in _effectList )
            {
                if ( effect.EnabledPlay )
                    effect.PlayEffect();
            }
        }

        private void OnDisable()
        {
        }

        void HandleOnPlay( string groupId )
        {
            foreach ( var effect in _effectList )
            {
                if ( effect.EnabledPlay )
                    continue;

                if ( string.IsNullOrEmpty( groupId ) || effect.GroupId.Equals( groupId ) )
                    effect.PlayEffect();
            }
        }

        void HandleOnStop( string groupId )
        {
            foreach ( var effect in _effectList )
            {
                if ( effect.EnabledPlay )
                    continue;

                if ( string.IsNullOrEmpty( groupId ) || effect.GroupId.Equals( groupId ) )
                    effect.StopEffect();
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor( typeof( UIEffectHolder ) )]
    public class UIEffectHolderEditor : Editor
    {
        // SerializedProperty _spEffectList;

        //private void OnEnable()
        //{
        //    _spEffectList = serializedObject.FindProperty( "_effectList" );
        //}

        public override void OnInspectorGUI()
        {
            UIEffectHolder holder = target as UIEffectHolder;

            DrawDefaultInspector();


            serializedObject.Update();



            foreach( var effect in holder._effectList )
            {
                GUILayout.Label( effect.GroupId );
                GUILayout.Label( effect.Owner.name );
                effect.SetEnable( GUILayout.Toggle( effect.EnabledPlay , "Enable Play" ) );
                EditorGUILayout.ObjectField( "Owner" , effect.Owner , typeof( GameObject ) , true );
                if ( GUILayout.Button( "Play" ) )
                {
                    effect.StopEffect();
                }
            }

            using ( new EditorGUI.DisabledGroupScope( Application.isPlaying ) )
            using ( new EditorGUILayout.HorizontalScope( EditorStyles.helpBox ) )
            {
                GUILayout.Label( "Edit" );

                if ( GUILayout.Button( "Search" , "ButtonLeft" ) )
                {
                    IUIEffect[] effectArray = ( target as UIEffectHolder ).GetComponentsInChildren<IUIEffect>();
                    Debug.Log( $"{effectArray.Length}" );

                    holder._effectList.Clear();
                    holder._effectList = effectArray.ToList();
                }
            }

            

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.EndHorizontal();



            serializedObject.ApplyModifiedProperties();
        }
    }
#endif



}
