using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class UIEffectTweenGroup : TweenGroup , IUIEffect
    {
        #region Implements
        [SerializeField] private bool _enablePlay;
        public bool EnabledPlay => _enablePlay;

        [SerializeField] private string _groupId;
        public string GroupId => _groupId;

        public bool _resetPlay;
        public bool ResetPlay => _resetPlay;

#if UNITY_EDITOR
        public GameObject Owner => gameObject;
        public void SetEnable( bool enable ) { _enablePlay = enable; }

        public void SetResetPlay( bool reset ) { _resetPlay = reset; }
#endif
        public void PlayEffect()
        {
            // Play();
        }

        public void StopEffect()
        {
        }

        public void PauseEffect()
        {
        }
        #endregion
    }
}
