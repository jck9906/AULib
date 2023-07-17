using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace AULib
{
    /// <summary>
    /// UI에 마우스 오버 상태인지 체크
    /// PhysicsRaycaster 사용시 지정된 레이어 에서 IsPointerOverGameObject가 항상 True로 와서 대체하기 위한 클래스
    /// 사용하려는 캔버스에 추가되어야 함
    /// </summary>
    [RequireComponent(typeof(EventTrigger))]
    public class MouseInputUIBlocker : MonoBehaviour
    {
        public static bool BlockedByUI;
        private EventTrigger eventTrigger;

        private void Start()
        {
            eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                EventTrigger.Entry enterUIEntry = new EventTrigger.Entry();
                // Pointer Enter
                enterUIEntry.eventID = EventTriggerType.PointerEnter;
                enterUIEntry.callback.AddListener((eventData) => { EnterUI(); });
                eventTrigger.triggers.Add(enterUIEntry);

                //Pointer Exit
                EventTrigger.Entry exitUIEntry = new EventTrigger.Entry();
                exitUIEntry.eventID = EventTriggerType.PointerExit;
                exitUIEntry.callback.AddListener((eventData) => { ExitUI(); });
                eventTrigger.triggers.Add(exitUIEntry);
            }
        }

        public void EnterUI()
        {
            if (!(Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed))
            {
                BlockedByUI = true;
            }
                
        }
        public void ExitUI()
        {
            BlockedByUI = false;
        }

    }
}




 
 