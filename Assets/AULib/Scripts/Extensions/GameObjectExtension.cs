using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// GameObject Ȯ�� �޼���
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class GameObjectExtension
    {

        /// <summary>
        /// GameObject�� �ش� ���۳�Ʈ�� ������ ���۳�Ʈ�� �����ϰ� ������ Add �� ����
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
        /// GameObject�� �ش� ���۳�Ʈ�� �ִ��� ���� ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }


        /// <summary>
        /// GameObject�� ���̾� ����
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name">������ ���̾� �̸�</param>
        /// <param name="changeChildren">���ϵ� ������Ʈ ���� ����</param>
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