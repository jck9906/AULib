using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// Vector 확장 메서드
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class VectorExtension
    {

        /// <summary>
        /// Vector의 X 값 셋팅
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 SetX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }

        /// <summary>
        /// Vector의 Y 값 셋팅
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 SetY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }

        /// <summary>
        /// Vector의 X 값에 더하기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return new Vector2(vector.x + x, vector.y);
        }

        /// <summary>
        /// Vector의 Y 값에 더하기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }


        /// <summary>
        /// Vector의 X 값 셋팅
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        /// <summary>
        /// Vector의 Y 값 셋팅
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 SetY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        /// <summary>
        /// Vector의 Z 값 셋팅
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        /// <summary>
        /// Vector의 X 값에 더하기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 AddX(this Vector3 vector, float x)
        {
            return new Vector3(vector.x + x, vector.y, vector.z);
        }

        /// <summary>
        /// Vector의 Y 값에 더하기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, vector.y + y, vector.z);
        }

        /// <summary>
        /// Vector의 Y 값에 더하기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, vector.z + z);
        }

        /// <summary>
        /// Vecto3를 Vector2로 변환
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 xy(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

     


        /// <summary>
        /// 가장 가까운 벡터 찾기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="otherVectors"></param>
        /// <returns></returns>
        public static Vector2 GetClosestVector2From(this Vector2 vector, Vector2[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector2.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }


        /// <summary>
        /// 가장 가까운 벡터 찾기
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="otherVectors"></param>
        /// <returns></returns>
        public static Vector3 GetClosestVector3From(this Vector3 vector, Vector3[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector3.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        // axisDirection - unit vector in direction of an axis (eg, defines a line that passes through zero)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnAxis(this Vector3 axisDirection, Vector3 point, bool isNormalized = false)
        {
            if (!isNormalized) axisDirection.Normalize();
            var d = Vector3.Dot(point, axisDirection);
            return axisDirection * d;
        }

        // lineDirection - unit vector in direction of line
        // pointOnLine - a point on the line (allowing us to define an actual line in space)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnLine(
            this Vector3 lineDirection, Vector3 point, Vector3 pointOnLine, bool isNormalized = false)
        {
            if (!isNormalized) lineDirection.Normalize();
            var d = Vector3.Dot(point - pointOnLine, lineDirection);
            return pointOnLine + (lineDirection * d);
        }
    }
}