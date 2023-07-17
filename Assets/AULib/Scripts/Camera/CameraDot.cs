using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    public static class CameraDot 
    {

        /// <summary>
        /// 카메라와 해당 벡터 위치의 내적 구하기
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="targetDirection"></param>
        /// <returns></returns>
      public static float GetDot(Transform cameraTransform, Vector3 targetDirection)
        {
            return Vector3.Dot(cameraTransform.TransformDirection(Vector3.forward), targetDirection);
        }


        /// <summary>
        /// 카메라와 해당 벡터 위치의 내적 구한 후 -1 또는 1로 변환하기
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="targetDirection"></param>
        /// <returns></returns>
        public static int GetDotToNegativeToOne(Transform cameraTransform, Vector3 targetDirection)
        {
            float dot = GetDot(cameraTransform, targetDirection);
            dot = (Mathf.Sign(dot) * Mathf.Ceil(Mathf.Abs(dot)));
            return (int)dot;
        }

        /// <summary>
        /// 카메라와 해당 벡터 위치의 내적 구한 후 0 또는 1로 변환하기
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="targetDirection"></param>
        /// <returns></returns>
        public static int GetDotToZeroToOne(Transform cameraTransform, Vector3 targetDirection)
        {
            int dot = GetDotToNegativeToOne(cameraTransform, targetDirection);
            dot = Mathf.Clamp(dot, 0, 1);
            return dot;
        }
    }
}
