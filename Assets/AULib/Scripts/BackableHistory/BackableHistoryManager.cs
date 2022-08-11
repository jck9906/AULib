using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{

    /// <summary>
    /// 디바이스 Back 버튼 입력에 따른 페이지 전환 컨텐츠 스택 관리
    /// </summary>
    public class BackableHistoryManager : MonoSingletonBase<BackableHistoryManager>, IBackable
    {

        public static List<BackableHandler> backables = new List<BackableHandler>();

        public static event Action OnBackInputWhenEmpty;

        public override void Init()
        {
            
        }







        public static void AddBackable(BackableHandler backable)
        {
            if (!backables.Contains(backable))
            {
                backables.Add(backable);
            }
        }


        public static void RemoveBackable(BackableHandler backable)
        {
            if (backables.Contains(backable))
            {
                backables.Remove(backable);
            }
        }

        #region Implements IBackable
        public void OnBackButtonInput()
        {
            
            if (backables.Count == 0)
            {
                OnBackInputWhenEmpty?.Invoke();
              //  CommonUIController.i.SetMessage(ALDataLoader.i.GetLocalizedUIText("COMMON_MSG_TITLE_NOTICE"), ALDataLoader.i.GetLocalizedUIText("COMMON_MSG_ASK_QUIT_GAME"), MessageBox.ButtonTypeParam.GetDefaultParamTwoButton(),
              //  okCallback: () =>
              // {
              //     Application.Quit();
              // },
              // cancelCallback: () =>
              //{

              //});
            }
            else
            {
                BackableHandler backableHandler = backables[backables.Count - 1];
                backableHandler.OnBackButtonInput();
            }

        }
        #endregion Implements IBackable
    }
}