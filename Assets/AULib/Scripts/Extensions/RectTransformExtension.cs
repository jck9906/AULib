using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    /// <summary>
    /// RectTransform »Æ¿Â
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Left set
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RectTransform Left(this RectTransform rt, float x)
        {
            rt.offsetMin = new Vector2(x, rt.offsetMin.y);
            return rt;
        }


        /// <summary>
        /// Right set
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RectTransform Right(this RectTransform rt, float x)
        {
            rt.offsetMax = new Vector2(-x, rt.offsetMax.y);
            return rt;
        }


        /// <summary>
        /// Bottom set
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RectTransform Bottom(this RectTransform rt, float y)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, y);
            return rt;
        }


        /// <summary>
        /// Top set
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RectTransform Top(this RectTransform rt, float y)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -y);
            return rt;
        }
    }
}
