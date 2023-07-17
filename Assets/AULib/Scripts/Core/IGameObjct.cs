using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    /// <summary>
    /// ���ӿ�����Ʈ Ÿ���� �������̽� 
    /// �ʿ信 ���� ��� �÷��ָ� ��
    /// </summary>
    public interface IGameObjct 
    {
        public GameObject gameObject { get; }
        public string name { get; set; }
        public Transform transform { get; }
    }
}
