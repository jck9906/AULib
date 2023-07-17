using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace AULib
{


    public delegate void ToggleItemEvent(int toggleIndex);
    /// <summary>
    /// 토글 아이템 이벤트 트리거
    /// </summary>
    public class ToggleItemEventTrigger : BaseBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //토글 그룹의 토글 옵션 인덱스
        private int _toggleIndex;

        public int ToggleIndex => _toggleIndex;



        public event ToggleItemEvent onToggleLeftClick;
        public event ToggleItemEvent onToggleRightClick;
        public event ToggleItemEvent onTogglePointerEnter;
        public event ToggleItemEvent onTogglePointerExit;



        /// <summary>
        /// //토글 그룹의 토글 옵션 인덱스 셋팅
        /// </summary>
        /// <param name="toggleIndex"></param>
        public void SetToggleIndex(int toggleIndex)
        {
            _toggleIndex = toggleIndex;
        }


        /// <summary>
        /// 마우스 포인터 엔터
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
        /// 마우스 포인터 아웃
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            onTogglePointerExit?.Invoke(_toggleIndex);
        }

       
        /// <summary>
        /// 마우스 포인터 클릭
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
