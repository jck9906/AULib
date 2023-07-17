using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// 인스펙터 이름 노출되는 필드 이름 변경 속성
    /// https://answers.unity.com/questions/603882/serializedproperty-isnt-being-detected-as-an-array.html
    /// </summary>
    public class LabelOverrideAttribute : PropertyAttribute
    {
        public string label;
        public LabelOverrideAttribute(string label)
        {
            this.label = label;
        }
    }
}
