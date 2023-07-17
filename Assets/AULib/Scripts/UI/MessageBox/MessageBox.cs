using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;


namespace AULib
{
    public partial class MessageBox : BaseBehaviour, IBlockerUsable, IBackable, ICommonUIElement
    {
        [SerializeField] RectTransform bodyRect;
        [SerializeField] TMP_Text caption;
        [SerializeField] TMP_Text Message;
        

        [SerializeField] Button buttonExit;
        [SerializeField] Button buttonOK;
        [SerializeField] Button buttonCancel;
        [SerializeField] Button buttonOther;
        [SerializeField] TMP_Text txtOK;
        [SerializeField] TMP_Text txtCancel;
        [SerializeField] TMP_Text txtOther;
        

        
        private static string defaultConfirmBtnMsg = "Confirm";
        private static string defaultCancelBtnMsg = "Cancel";


        private Action _okCallback;
        
        private Action _cancelCallback;
        private Action _otherCallback;

     

        public enum eMB_TYPE
        {
            MB_OK_CANCEL,
            MB_OK,
            MB_OK_CANCEL_OTHER
        }

        public enum eMB_ORDER_TYPE
        {
            OK,
            OK_CANCEL,
            CANCEL_OK,
            OK_CANCEL_OTHER,
            OTHER_OK_CANCEL
        }

        eMB_TYPE mbType;
        eMB_ORDER_TYPE mbOrderType;



        void OnEnable()
        {

        }

        void OnDisable()
        {

        }

        /// <summary>
        /// 확인, 취소 버튼 기본 텍스트 셋팅
        /// </summary>
        /// <param name="confirm"></param>
        /// <param name="cancel"></param>
        public static void SetDefaultBtnMsg(string confirm, string cancel)
        {
            defaultConfirmBtnMsg = confirm;
            defaultCancelBtnMsg = cancel;
        }
        public void Init()
        {
            Hide();

        }

        public void SetMessage(string strCaption, string strMessage, ButtonTypeParam buttonTypeParam, Action okCallback = null, Action cancelCallback = null, Action otherCallback = null)
        {
            SetMessageType(buttonTypeParam.mbType, buttonTypeParam.mbOrderType);

            CreateBlocker();

            SetMessageText(strCaption, strMessage);

            SetBtnText(buttonTypeParam);
            InitBtnAction();
            SetBtnCallback(okCallback, cancelCallback, otherCallback);
            SetBtnAction();
            SetBtnActive();
            SetBtnOrder();

            SetMessageInput(InputTypeParam.GetNullParam());

            gameObject.SetActive(true);


        }

        private void CreateBlocker()
        {
            if (mbType != eMB_TYPE.MB_OK)
            {
                BlockerBuilder.CreateBlocker(GetComponentInParent<Canvas>().rootCanvas, bodyRect.GetComponent<Canvas>(), this);
            }
            else
            {
                BlockerBuilder.CreateBlocker(GetComponentInParent<Canvas>().rootCanvas, bodyRect.GetComponent<Canvas>());
            }

        }




        private void SetMessageType(eMB_TYPE type, eMB_ORDER_TYPE mbOrderType)
        {
            mbType = type;
            this.mbOrderType = mbOrderType;
        }

        private void SetMessageText(string strCaption, string strMessage)
        {
            caption.text = strCaption;
            Message.text = strMessage;
        }

        private void SetBtnText(ButtonTypeParam buttonTypeParam)
        {
            txtOK.text = buttonTypeParam.confirm;
            txtCancel.text = buttonTypeParam.cancel;
            txtOther.text = buttonTypeParam.other;
        }


        private void InitBtnAction()
        {
            buttonExit.onClick.RemoveAllListeners();
            buttonOK.onClick.RemoveAllListeners();
            buttonCancel.onClick.RemoveAllListeners();
            buttonOther.onClick.RemoveAllListeners();
        }

        private void SetBtnCallback(Action okCallback = null, Action cancelCallback = null, Action otherCallback = null)
        {
            _okCallback = okCallback;
            _cancelCallback = cancelCallback;
            _otherCallback = otherCallback;
        }

       

        private void SetBtnAction()
        {
            buttonExit.onClick.AddListener(HandleOnClickExit);
            buttonOK.onClick.AddListener(HandleOnClickOK);
            buttonCancel.onClick.AddListener(HandleOnClickCancel);
            buttonOther.onClick.AddListener(HandleOnClickOther);
        }

       

        private void SetBtnActive()
        {
            buttonOK.Select();
            buttonOther.gameObject.SetActive(false);
            buttonExit.gameObject.SetActive(false);

            if (mbType == eMB_TYPE.MB_OK)
            {
                buttonCancel.gameObject.SetActive(false);                
                //RectTransform rt = buttonOK.GetComponent<RectTransform>();
                //rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);
            }
            else if (mbType == eMB_TYPE.MB_OK_CANCEL)
            {
                buttonCancel.gameObject.SetActive(true);
                buttonExit.gameObject.SetActive(true);
                //RectTransform rt = buttonOK.GetComponent<RectTransform>();
                //rt.anchoredPosition = new Vector2(-210, rt.anchoredPosition.y);
            }
            else
            {
                buttonOther.gameObject.SetActive(true);
                buttonCancel.gameObject.SetActive(true);
                buttonExit.gameObject.SetActive(true);
            }
        }


        private void SetBtnOrder()
        {
            RectTransform rectOK = buttonOK.transform as RectTransform;
            RectTransform rectCancel = buttonCancel.transform as RectTransform;
            RectTransform rectOther = buttonOther.transform as RectTransform;

            switch (mbOrderType)
            {
                case eMB_ORDER_TYPE.OK:
                    break;

                case eMB_ORDER_TYPE.OK_CANCEL:
                    rectOK.SetSiblingIndex(0);
                    rectCancel.SetSiblingIndex(1);
                    break;

                case eMB_ORDER_TYPE.CANCEL_OK:
                    rectCancel.SetSiblingIndex(0);
                    rectOK.SetSiblingIndex(1);
                    break;

                case eMB_ORDER_TYPE.OK_CANCEL_OTHER:
                    rectOK.SetSiblingIndex(0);
                    rectCancel.SetSiblingIndex(1);
                    rectOther.SetSiblingIndex(2);
                    break;

                case eMB_ORDER_TYPE.OTHER_OK_CANCEL:
                    rectOther.SetSiblingIndex(0);
                    rectCancel.SetSiblingIndex(1);
                    rectOK.SetSiblingIndex(2);
                    break;

                default:
                    break;
            }
        }



        private void HandleOnClickExit()
        {
            Hide();
            _cancelCallback?.Invoke();
        }

        private void HandleOnClickOK()
        {
            Hide();
            _okCallback?.Invoke();
        }

       

        private void HandleOnClickCancel()
        {
            Hide();
            _cancelCallback?.Invoke();
        }

        private void HandleOnClickOther()
        {
            Hide();
            _otherCallback?.Invoke();
        }


        public override void Hide()
        {
            BlockerBuilder.Hide(this, false);
            base.Hide();
        }

        public void Close()
        {
            if (mbType != eMB_TYPE.MB_OK)
            {
                Hide();
            }
        }


        #region Implements IBackable
        public void OnBackButtonInput()
        {
            // One Button 일 경우 OK 처리.
            if ( mbType == eMB_TYPE.MB_OK )
                HandleOnClickOK();
            else
                HandleOnClickCancel();
        }
        #endregion Implements IBackable
    }


}

