using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// LoadingSceneMannager�� ���� �ε� �� �� ������
    /// </summary>
    public interface ISceneLoadListener
    {
        public void OnSceneLoad();
        public void OnSceneUnLoad();
    }


    public class SceneLoadListener : MonoBehaviour, ISceneLoadListener
    {
        public virtual void OnSceneLoad()
        {
            
        }

        public virtual void OnSceneUnLoad()
        {
            
        }
    }
}