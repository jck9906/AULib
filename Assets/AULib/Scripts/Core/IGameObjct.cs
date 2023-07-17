using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    /// <summary>
    /// 게임오브젝트 타입의 인터페이스 
    /// 필요에 따라 멤버 늘려주면 됨
    /// </summary>
    public interface IGameObjct 
    {
        public GameObject gameObject { get; }
        public string name { get; set; }
        public Transform transform { get; }
    }
}
