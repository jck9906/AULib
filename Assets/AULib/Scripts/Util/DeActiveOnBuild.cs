using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// ���� ���¿����� ���ӿ�����Ʈ ��Ȱ��
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
