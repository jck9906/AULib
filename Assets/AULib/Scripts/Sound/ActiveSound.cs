using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    /// <summary>
    /// 액티브 On 되었을때 효과음 출력
    /// </summary>
    public class ActiveSound : BaseBehaviour
    {

        [SerializeField] SoundEffectEnum clip;

        private bool _isDisable = false;

        private void OnEnable()
        {
            if (_isDisable)
            {
                SoundManager.i.PlayAudio(clip);
                _isDisable = false;
            }
        }

        private void OnDisable()
        {
            _isDisable = true;
        }
    }
}
