using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// �˾� ����
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
        /// �˾� ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T OpenPopup<T>(string popPath, Action<T> onOpenAction = null,  Action<T, bool> onCloseAction = null ) where T : IPopup
        {
            IPopup popup; ;

            //Ǯ���� ã�Ƽ� ������ ����
            if (_popups.TryGetValue(popPath, out popup))
            {
                return _OpenPopup(popup, onOpenAction, onCloseAction);                
            }
            //Ǯ�� ������ ���� ���� �� Ǯ�� �ִ´�.
            popup = PopupFactory.Get<T>(popPath, parent);            
            _OpenPopup(popup, onOpenAction, onCloseAction);
            

            _popups.Add(popPath, popup);

            return (T)popup;            
        }

            /// <summary>
            /// �˾� �ݱ�
            /// </summary>
            /// <param name="pop"></param>
            /// <param name="isDestroy"> ���� Destroy ��ų ������</param>
            /// <returns></returns>
            public bool ClosePopup(string popPath, bool isDestroy)
        {
            IPopup popup;

            
            //Ǯ���� ã�Ƽ� ������ ����
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