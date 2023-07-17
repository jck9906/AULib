using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using Coffee.UIExtensions;

namespace AULib
{
    public class UIEffectEffect : UIEffect//, IUIEffect
    {
        #region Implements
        [SerializeField] private bool _enablePlay;
        public bool EnablePlay => _enablePlay;

        [SerializeField] private string _groupId;
        public string GroupId => _groupId;
#if UNITY_EDITOR
        public GameObject Owner => gameObject;
        public void SetEnable( bool enable ) { _enablePlay = enable; }
#endif
        public void PlayEffect( bool reset )
        {
        }

        public void PauseEffect( bool reset )
        {
        }
        #endregion
    }
}
