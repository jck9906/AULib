using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// 메세지 박스 입력 처리 분리
    /// </summary>
    public partial class MessageBox : BaseBehaviour
    {
        [SerializeField] GameObject inputArea;
        [SerializeField] TMP_InputField inputMsg;
        [SerializeField] ToastMessage toastMessage;

        private Action<String> _okInputCallback;


        InputTypeParam inputParam;

        public enum eInputConfirmError
        {
            Clear,
            ShortLength, //입력 문자 길이 부족
            OverLength, // 입력 문자 길이 넘침
            IncludeBanned // 금지어 포함
        }

        /// <summary>
        /// 인풋 확인 딜리게이트
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate (bool isPass, eInputConfirmError errorType, string errorMessage) GetInputConfirm(string msg);


        public void SetMessageWithInput(string strCaption, string strMessage, ButtonTypeParam buttonTypeParam, InputTypeParam inputParam, Action<string> okCallback = null, Action cancelCallback = null, Action otherCallback = null, GetInputConfirm inputConfirm = null)
        {
            SetMessageType(buttonTypeParam.mbType, buttonTypeParam.mbOrderType);

            CreateBlocker();

            SetMessageText(strCaption, strMessage);

            SetBtnText(buttonTypeParam);
            InitBtnAction();
            SetBtnCallback(okCallback, cancelCallback, otherCallback);

            buttonOK.onClick.AddListener(delegate { HandleOnClickOKWithInput(inputConfirm); });
            buttonCancel.onClick.AddListener(HandleOnClickCancel);
            buttonOther.onClick.AddListener(HandleOnClickOther);
            //buttonOther.onClick.AddListener(delegate { OnClickOtherWithInput(otherCallback, inputConfirm); });

            SetBtnActive();
            SetBtnOrder();

            SetMessageInput(inputParam);


            gameObject.SetActive(true);
        }




        private bool CheckInputValid(GetInputConfirm inputConfirm)
        {
            
            var confirmResult = inputConfirm(inputMsg.text);

            if (!confirmResult.isPass)
            {

                toastMessage.SetDuration(ToastMessage.SHOW_DURATION_NORMAL).SetMessage(confirmResult.errorMessage);                
                return false;
            }

            return true;
        }

        private void SetBtnCallback(Action<String> okCallback = null, Action cancelCallback = null, Action otherCallback = null)
        {
            _okInputCallback = okCallback;
            _cancelCallback = cancelCallback;
            _otherCallback = otherCallback;
        }

        private void SetMessageInput(InputTypeParam inputParam)
        {
            this.inputParam = inputParam;
            if (!inputParam.isNullParam)
            {
                inputArea.gameObject.SetActive(true);
                inputMsg.characterLimit = inputParam.maxLength;
                if (string.IsNullOrEmpty(inputParam.defaultMessage))
                {
                    var hintField = inputMsg.placeholder.GetComponent<TMP_Text>();
                    hintField.text = inputParam.hintMessage;
                    inputMsg.text = string.Empty;
                }
                else
                {
                    inputMsg.text = inputParam.defaultMessage;
                }

            }
            else
            {
                inputArea.gameObject.SetActive(false);
            }
        }


        private void HandleOnClickOKWithInput(GetInputConfirm inputConfirm)
        {
            if (CheckInputValid(inputConfirm))
            {
                Hide(); buttonOK.onClick.RemoveAllListeners();
                _okInputCallback?.Invoke(inputMsg.text);
            }
        }
    }
}
