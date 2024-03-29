using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// GameObject 확장 메서드
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class GameObjectExtension
    {

        

        /// <summary>
        /// GameObject에 해당 컴퍼넌트가 있으면 컴퍼넌트를 리턴하고 없으면 Add 후 리턴
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T component)) {
                return component;
            }
            else
            {
                return gameObject.AddComponent<T>();
            }
            //var component = gameObject.GetComponent<T>();
            //if (component == null) component = gameObject.AddComponent<T>();
            //return component;
        }



        /// <summary>
        /// GameObject에 해당 컴퍼넌트가 있는지 여부 리턴
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {

            //return gameObject.GetComponent<T>() != null;
            return gameObject.TryGetComponent<T>(out T component);
        }






        //레이어 변경 시 예외조건 설정
        public delegate bool ChangeLayerExceptCondition(int layer);



        /// <summary>
        /// GameObject에 레이어 변경
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name">변경할 레이어 이름</param>
        /// <param name="changeChildren">차일드 오브젝트 변경 여부</param>
        public static void ChangeLayers(this GameObject gameObject, string name, bool changeChildren, ChangeLayerExceptCondition condition = null)
        {
            gameObject.layer = LayerMask.NameToLayer(name);
            if (changeChildren)
            {
                ChangeLayersRecursively(gameObject.transform, name, condition);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SkinnedMeshRenderer GetSkinnedMeshRenderer(this GameObject gameObject, string name)
        {
            SkinnedMeshRenderer[] smrs = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);

            for (int i = 0; i < smrs.Length; i++)
            {
                if (smrs[i].name == name)
                    return smrs[i];
            }

            return null;
        }



        private static void ChangeLayersRecursively(Transform trans, string name, ChangeLayerExceptCondition condition = null)
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in trans)
            {
                if (condition != null)
                {
                    if (condition(child.gameObject.layer))
                    {
                        continue;
                    }
                }
                //if ( child.gameObject.layer == LayerMask.NameToLayer( "Trigger" ) || child.gameObject.layer == LayerMask.NameToLayer( "Detection" ) )
                //    continue;

                child.gameObject.layer = LayerMask.NameToLayer( name );
                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if ( _HasChildren != null )
                {
                    ChangeLayersRecursively( child, name, condition);
                }
            }
        }
    }
}