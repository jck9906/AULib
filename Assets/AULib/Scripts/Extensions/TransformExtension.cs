using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// Transform 확장 메서드
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class TransformExtension
    {

        /// <summary>
        /// 자식 오브젝트 제거
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
        /// 트랜스폼 정보 리셋
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 오브젝트 이름으로 트랜스폼 찾기
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform GetTransformByName(this Transform transform, string name, bool includeInactive = false)
        {
            Transform[] trs = transform.GetComponentsInChildren<Transform>(includeInactive);
            foreach (var tr in trs)
            {
                if (tr.name == name)
                    return tr;
            }
            return null;
        }


    }
}