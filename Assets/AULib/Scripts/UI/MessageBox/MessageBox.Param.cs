using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// 메세지 박스 파라미터 관련 부분 분리
    /// </summary>
    public partial class MessageBox : BaseBehaviour
    {


        /// <summary>
        /// 메세지 입력 형태일 시 파라미터
        /// </summary>
        public struct InputTypeParam
        {
            public bool isNullParam;
            public int minLength;
            public int maxLength;
            public string defaultMessage;
            public string hintMessage;

            public static InputTypeParam GetNullParam()
            {
                InputTypeParam nullParam = new InputTypeParam();
                nullParam.isNullParam = true;
                return nullParam;
            }


            public InputTypeParam(int minLength, int maxLength, string defaultMessage, string hintMessage)
            {
                this.minLength = minLength;
                this.maxLength = maxLength;
                this.defaultMessage = defaultMessage;
                this.hintMessage = hintMessage;
                isNullParam = false;
            }
        }

        public struct ButtonTypeParam
        {
            public eMB_TYPE mbType;
            public eMB_ORDER_TYPE mbOrderType;
            public string confirm;
            public string cancel;
            public string other;


            public static ButtonTypeParam GetDefaultParamTwoButton()
            {
                ButtonTypeParam defaultParam = new ButtonTypeParam();
                defaultParam.mbType = eMB_TYPE.MB_OK_CANCEL;
                defaultParam.mbOrderType = eMB_ORDER_TYPE.CANCEL_OK;
                defaultParam.confirm = defaultConfirmBtnMsg/*ALDataLoader.i.GetLocalizedUIText("COMMON_MSG_BTN_CONFIRM")*/;
                defaultParam.cancel = defaultCancelBtnMsg/*ALDataLoader.i.GetLocalizedUIText("COMMON_MSG_BTN_CANCEL")*/;

                return defaultParam;
            }


            public static ButtonTypeParam GetDefaultParamOneButton()
            {
                ButtonTypeParam defaultParam = new ButtonTypeParam();
                defaultParam.mbType = eMB_TYPE.MB_OK;
                defaultParam.mbOrderType = eMB_ORDER_TYPE.OK;
                defaultParam.confirm = defaultConfirmBtnMsg/*ALDataLoader.i.GetLocalizedUIText("COMMON_MSG_BTN_CONFIRM")*/;

                return defaultParam;
            }

            public ButtonTypeParam(eMB_TYPE mbType, eMB_ORDER_TYPE mbOrderType, string confirm, string cancel, string other)
            {
                this.mbType = mbType;
                this.mbOrderType = mbOrderType;
                this.confirm = confirm;
                this.cancel = cancel;
                this.other = other;
            }
        }


    }
}
