using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// String 확장 클래스
    /// </summary>
    public static class StringExtension 
    {

        /// <summary>
        /// 클립보드 복사
        /// </summary>
        /// <param name="str"></param>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}
