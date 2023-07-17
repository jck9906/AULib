using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    /// <summary>
    /// ��Ƽ�� On �Ǿ����� ȿ���� ���
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
