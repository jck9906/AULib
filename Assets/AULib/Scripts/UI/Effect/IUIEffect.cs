using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AULib
{
    public interface IUIEffect
    {
        public bool EnabledPlay { get; }
        public bool ResetPlay { get; }
        public string GroupId { get; }



#if UNITY_EDITOR
        public GameObject Owner { get; }

        public void SetEnable( bool enable );

        public void SetResetPlay( bool reset );
#endif

        public void PlayEffect();
        public void StopEffect();
        public void PauseEffect();
    }
}
