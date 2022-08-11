using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// Transform Ȯ�� �޼���
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class TransformExtension
    {

        /// <summary>
        /// �ڽ� ������Ʈ ����
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Ʈ������ ���� ����
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}