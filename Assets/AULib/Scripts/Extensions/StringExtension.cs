using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// String Ȯ�� Ŭ����
    /// </summary>
    public static class StringExtension 
    {

        /// <summary>
        /// Ŭ������ ����
        /// </summary>
        /// <param name="str"></param>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}
