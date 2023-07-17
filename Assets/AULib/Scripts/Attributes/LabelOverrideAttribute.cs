using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// �ν����� �̸� ����Ǵ� �ʵ� �̸� ���� �Ӽ�
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
