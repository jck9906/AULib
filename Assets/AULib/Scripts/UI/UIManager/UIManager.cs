using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AULib
{

    /// <summary>
    /// UI ���� ����
    /// ������ �ʿ��� UI �� RegisterUIObject ����  _uiObjectDic�� ��� �Ǿ�� �Ѵ�.
    /// ������ �ʿ��� UI �� IUIObject�� �����ؾ� �Ѵ�.
    /// ������ �ʿ��� UI �� UIObjectRegister ���۳�Ʈ�� �־�� �Ѵ�.
    /// </summary>
    public class UIManager : MonoSingleton<UIManager>
    {
        
        //�������� ���� UI��(�޼��� �ڽ�, �佺Ʈ �޼���,...)
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
        /// UIObject ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiObject"></param>
        public void RegisterUIObject<T>(T uiObject) where T : IUIObject
        {

            if (_uiObjectDic.TryAdd(uiObject.GetType(), uiObject))
            {
                Debug.Log($"UIManager�� �߰� ����. - {uiObject}");
            }
            else
            {
                Debug.LogWarning($"UIManager�� �̹� ���� Ű�� �߰� �Ǿ� �ֽ��ϴ�. - {uiObject}");
            }            
        }



        /// <summary>
        /// UIObject ��� ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiObject"></param>
        public void UnRegisterUIObject<T>(T uiObject) where T : IUIObject
        {
            if (_uiObjectDic.ContainsKey(uiObject.GetType()))
            {
                _uiObjectDic.Remove(uiObject.GetType());
                Debug.Log($"UIManager�� ��� ���� ����. - {uiObject}");
            }
            else
            {
                Debug.LogWarning($"UIManager�� �ش� Ű�� �����ϴ�. - {uiObject}");
            }
        }


        /// <summary>
        /// UIObject ����
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
