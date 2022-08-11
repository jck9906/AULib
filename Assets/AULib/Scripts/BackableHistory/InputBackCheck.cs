using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace AULib
{

    /// <summary>
    /// 디바이스 Back 버튼 입력 체크
    /// </summary>
    public class InputBackCheck : MonoSingletonBase<InputBackCheck>
    {


        public override void Init()
        {
            
        }


        /// <summary>
        /// 디바이스 Back or Esc 인풋 - 인스펙터 PlayerInput 에서 셋팅 됨
        /// </summary>
        /// <param name="obj"></param>
        public void OnBackInput(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                //Debug.Log("Back key input!!");
                BackableHistoryManager.i.OnBackButtonInput();
            }
        }





        public void OnCancelInput(InputAction.CallbackContext obj)
        {

            if (obj.performed)
            {
                Debug.Log("Cancel key input!!");
            }
        }
    }
}