using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace AULib
{


    public delegate void ToggleItemEvent(int toggleIndex);
    /// <summary>
    /// ��� ������ �̺�Ʈ Ʈ����
    /// </summary>
    public class ToggleItemEventTrigger : BaseBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //��� �׷��� ��� �ɼ� �ε���
        private int _toggleIndex;

        public int ToggleIndex => _toggleIndex;



        public event ToggleItemEvent onToggleLeftClick;
        public event ToggleItemEvent onToggleRightClick;
        public event ToggleItemEvent onTogglePointerEnter;
        public event ToggleItemEvent onTogglePointerExit;



        /// <summary>
        /// //��� �׷��� ��� �ɼ� �ε��� ����
        /// </summary>
        /// <param name="toggleIndex"></param>
        public void SetToggleIndex(int toggleIndex)
        {
            _toggleIndex = toggleIndex;
        }


        /// <summary>
        /// ���콺 ������ ����
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!(Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed))
            {
                onTogglePointerEnter?.Invoke(_toggleIndex);
            }            
        }


        /// <summary>
        /// ���콺 ������ �ƿ�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            onTogglePointerExit?.Invoke(_toggleIndex);
        }

       
        /// <summary>
        /// ���콺 ������ Ŭ��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button.Equals(PointerEventData.InputButton.Right))
            {
                onToggleRightClick?.Invoke(_toggleIndex);
            }
            else if (eventData.button.Equals(PointerEventData.InputButton.Left))
            {
                onToggleLeftClick?.Invoke(_toggleIndex);
            }
        }
    }
}
