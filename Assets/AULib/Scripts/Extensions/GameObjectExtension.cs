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
            var component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            return component;
        }



        /// <summary>
        /// GameObject에 해당 컴퍼넌트가 있는지 여부 리턴
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }


        /// <summary>
        /// GameObject에 레이어 변경
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name">변경할 레이어 이름</param>
        /// <param name="changeChildren">차일드 오브젝트 변경 여부</param>
        public static void ChangeLayers(this GameObject gameObject, string name, bool changeChildren)
        {
            gameObject.layer = LayerMask.NameToLayer(name);
            if (changeChildren)
            {
                ChangeLayersRecursively(gameObject.transform, name);
            }
        }



        private static void ChangeLayersRecursively(Transform trans, string name)
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in trans)
            {
                ChangeLayersRecursively(child, name);
            }
        }
    }
}