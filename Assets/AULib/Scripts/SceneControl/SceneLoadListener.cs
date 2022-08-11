using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// LoadingSceneMannager를 통해 로딩 된 씬 리스너
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