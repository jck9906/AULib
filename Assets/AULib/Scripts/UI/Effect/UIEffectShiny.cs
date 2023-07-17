using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using Coffee.UIExtensions;

namespace AULib
{
    public class UIEffectShiny : UIShiny , IUIEffect
    {
        #region Implements
        [SerializeField][Tooltip( "������Ʈ Ȱ��ȭ�� ����ϱ�" )]
        private bool _enablePlay;        
        public bool EnabledPlay => _enablePlay;

        
        [SerializeField][Tooltip( "����� ó������ ����" )]
        private bool _resetPlay;
        public bool ResetPlay => _resetPlay;


        [SerializeField] private string _groupId;
        public string GroupId => _groupId;

        

#if UNITY_EDITOR
        public GameObject Owner => gameObject;
        public void SetEnable( bool enable ) { _enablePlay = enable; }

        public void SetResetPlay( bool reset ) { _resetPlay = reset; }
#endif
        public void PlayEffect()
        {
            // Play( ResetPlay );
            // Stop( ResetPlay );
        }

        public void StopEffect()
        {
        }        
        public void PauseEffect()
        {

        }

        //protected override void OnEnable()
        //{
        //    base.OnEnable();
        //    if( EnabledPlay )
        //        Play( effectPlayer.initialPlayDelay == 0 && ResetPlay );
        //}

        #endregion
    }
}
