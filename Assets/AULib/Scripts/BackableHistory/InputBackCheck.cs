using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace AULib
{

    /// <summary>
    /// ����̽� Back ��ư �Է� üũ
    /// </summary>
    public class InputBackCheck : MonoSingletonBase<InputBackCheck>
    {


        public override void Init()
        {
            
        }


        /// <summary>
        /// ����̽� Back or Esc ��ǲ - �ν����� PlayerInput ���� ���� ��
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