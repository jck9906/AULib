using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// 빌드 상태에서는 게임오브젝트 비활성
    /// </summary>
    public class DeActiveOnBuild : BaseBehaviour
    {
        protected override void Awake() 
    	{
    		base.Awake();
#if !UNITY_EDITOR
            gameObject.SetActive(false);
#endif

        }
	
    	
    }
}
