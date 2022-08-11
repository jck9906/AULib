using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// 팝업 관리
    /// </summary>
    public class PopupManager : MonoSingleton<PopupManager>
    {


        [SerializeField] Transform parent;


        private Dictionary<string, IPopup> _popups;
        public override void Init() 
        {
            _popups = new Dictionary<string, IPopup>();
        }


        /// <summary>
        /// 팝업 열기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T OpenPopup<T>(string popPath, Action<T> onOpenAction = null,  Action<T, bool> onCloseAction = null ) where T : IPopup
        {
            IPopup popup; ;

            //풀에서 찾아서 있으면 리턴
            if (_popups.TryGetValue(popPath, out popup))
            {
                return _OpenPopup(popup, onOpenAction, onCloseAction);                
            }
            //풀에 없으면 새로 생성 후 풀에 넣는다.
            popup = PopupFactory.Get<T>(popPath, parent);            
            _OpenPopup(popup, onOpenAction, onCloseAction);
            

            _popups.Add(popPath, popup);

            return (T)popup;            
        }

            /// <summary>
            /// 팝업 닫기
            /// </summary>
            /// <param name="pop"></param>
            /// <param name="isDestroy"> 실제 Destroy 시킬 것인지</param>
            /// <returns></returns>
            public bool ClosePopup(string popPath, bool isDestroy)
        {
            IPopup popup;

            
            //풀에서 찾아서 있으면 리턴
            if (_popups.TryGetValue(popPath, out popup))
            {
                if (popup.IsActive())
                {
                    popup.Close(false);
                    return true;
                }
            }
            
            return false;
        }








        private T _OpenPopup<T>(IPopup popup, Action<T> onOpenAction = null, Action<T, bool> onCloseAction = null)
        {
            
            popup.Open(() =>
            {
                Debug.Log("Pop opened by manager");
                onOpenAction?.Invoke((T)popup);
            }, (isConfirm) =>
            {
                onCloseAction?.Invoke((T)popup, isConfirm);
            });

            popup.GetRectTransform().SetAsLastSibling();
            return (T)popup;
        }
    }
}