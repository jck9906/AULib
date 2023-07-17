using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AULib
{

    /// <summary>
    /// UI 통합 관리
    /// 관리가 필요한 UI 는 RegisterUIObject 통해  _uiObjectDic에 등록 되어야 한다.
    /// 관리가 필요한 UI 는 IUIObject를 구현해야 한다.
    /// 관리가 필요한 UI 는 UIObjectRegister 컴퍼넌트가 있어야 한다.
    /// </summary>
    public class UIManager : MonoSingleton<UIManager>
    {
        
        //공통으로 쓰는 UI들(메세지 박스, 토스트 메세지,...)
        [SerializeField] CommonUIController _common;
        public CommonUIController Common => _common;


        [SerializeField] PopupManager _popupManager;
        public PopupManager PopupManager => _popupManager;

        [SerializeField] CanvasGroup _CameraFade;
        [SerializeField] float FadeTime;

        private Dictionary<Type, IUIObject> _uiObjectDic = new();
        private Stack<GameObject> _uiStack = new();

        public override void Init()
        {
            base.Init();
            _uiStack.Clear();
        }






        /// <summary>
        /// UIObject 등록
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiObject"></param>
        public void RegisterUIObject<T>(T uiObject) where T : IUIObject
        {

            if (_uiObjectDic.TryAdd(uiObject.GetType(), uiObject))
            {
                Debug.Log($"UIManager에 추가 성공. - {uiObject}");
            }
            else
            {
                Debug.LogWarning($"UIManager에 이미 같은 키가 추가 되어 있습니다. - {uiObject}");
            }            
        }



        /// <summary>
        /// UIObject 등록 해제
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiObject"></param>
        public void UnRegisterUIObject<T>(T uiObject) where T : IUIObject
        {
            if (_uiObjectDic.ContainsKey(uiObject.GetType()))
            {
                _uiObjectDic.Remove(uiObject.GetType());
                Debug.Log($"UIManager에 등록 해제 성공. - {uiObject}");
            }
            else
            {
                Debug.LogWarning($"UIManager에 해당 키가 없습니다. - {uiObject}");
            }
        }


        /// <summary>
        /// UIObject 리턴
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUIObject<T>() where T : IUIObject
        {
            return (T)_uiObjectDic[typeof(T)];
        }

        public bool FindUIObject<T>() where T : IUIObject
        {
            return _uiObjectDic.ContainsKey( typeof( T ) );
        }

        public void CameraFadeIn(TweenCallback callback)
        {
            _CameraFade.DOFade( 1, FadeTime ).OnComplete( callback );
        }

        public void CameraFadeOut()
        {            
            _CameraFade.DOFade( 0, FadeTime*2f );
        }     
        
        public void UIStackAdd(GameObject uiObj)
        {
            if ( _uiStack.Count > 0 )
            {
                _uiStack.Peek().SetActive( false );
            }

            _uiStack.Push( uiObj );
        }

        public void UIStackDel(GameObject uiObj)
        {
            if( _uiStack.Peek() == uiObj )
            {
                _uiStack.Pop();

                if( _uiStack.Count > 0 )
                {
                    _uiStack.Peek().SetActive( true );
                }
            }
        }

        public void UIStackClear()
        {
            for ( int i = 0 ; i < _uiStack.Count ; ++i )
            {
                Destroy( _uiStack.Peek() );
            }
        }
    }
}
