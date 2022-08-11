using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AULib
{

    public interface IPopup
    {
        void Open(Action onOpenAction = null, Action<bool> onCloseAction = null);
        void Close(bool isConfirm);
        RectTransform GetRectTransform();

        bool IsActive();
    }


    /// <summary>
    /// �˾� â ���̽� Ŭ����
    /// </summary>
    public abstract class PopupBase<T> : UIBehaviour, IPopup, IBackable where T : IPopup
    {

        [SerializeField] protected Button btnExit;

        T thisT;
        //�˾��� ���Ⱦ�����
        protected bool _isOpened;
        public bool IsOpened => _isOpened;

        #region events
        //����׼� 
        private Action _onOpenAction;
        //�ݱ�׼� : bool - Ȯ���̸� true, ��Ҹ� false
        private Action<bool> _onCloseAction;

        public event Action<T> OnPopOpened;
        #endregion events

        protected override void Awake()
        {
            base.Awake();

            thisT = GetComponent<T>();
            btnExit.onClick.AddListener(OnClickExit);            
        }

       

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }



        #region publc method


        /// <summary>
        /// �˾� ����
        /// </summary>
        public virtual void Open(Action onOpenAction = null, Action<bool> onCloseAction = null)
        {   
            _onOpenAction = onOpenAction;
            _onCloseAction = onCloseAction;
            gameObject.SetActive(true);
            OnOpen();

            _onOpenAction?.Invoke();
            OnPopOpened?.Invoke(GetComponent<T>());
            _isOpened = true;
        }


        /// <summary>
        /// �˾� �ݱ�
        /// </summary>
        public virtual void Close(bool isConfirm)
        {
            OnBeforeClose();
            gameObject.SetActive(false);

            _onCloseAction?.Invoke(isConfirm);
            _onCloseAction = null;

            
        }


        #endregion publc method

        #region Implements IBackable
        public void OnBackButtonInput()
        {
            OnClickExit();
        }
        #endregion Implements IBackable










        protected virtual void OnClickExit()
        {
            if (CheckPopCloseCondition())
            {
                Close(false);
            }
        }




        #region abstract
        /// <summary>
        /// �˾� ���� �� ó��
        /// </summary>
        protected abstract void OnOpen();

        /// <summary>
        /// �˾� �ݱ� ���� Ȯ��
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckPopCloseCondition();

        /// <summary>
        /// �˾� �ݱ� �� ó��
        /// </summary>
        protected abstract void OnBeforeClose();

        public RectTransform GetRectTransform()
        {
            return transform as RectTransform;
        }

        #endregion abstract









    }


}