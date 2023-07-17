using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEngine.Events;

namespace AULib
{
    /// <summary>
    /// 전역 이벤트 관리 
    /// </summary>
    public static class EventBus 
    {

        /// <summary>
        /// 이벤트 타입
        /// </summary>
        public enum eGameEvent
        {
            FullWindow  ,
            OpenUI      ,
            PointerEnter,
            PointerOut
        }

        private static Dictionary<eGameEvent, UnityEvent<IEventBusParam>> _events = new();






        /// <summary>
        /// 이벤트 구독
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        public static void Subscribe(eGameEvent eventType, UnityAction<IEventBusParam> listener)
        {
            UnityEvent<IEventBusParam> thisEvent;

            if (_events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);

            }
            else
            {
                thisEvent = new UnityEvent<IEventBusParam>();
                thisEvent.AddListener(listener);
                _events.Add(eventType, thisEvent);
            }
        }


        /// <summary>
        /// 이벤트 구독 해제
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        public static void UnSubscribe(eGameEvent eventType, UnityAction<IEventBusParam> listener)
        {
            if (_events.TryGetValue(eventType, out var thisEvent))
            {
                thisEvent.RemoveListener(listener);

            }
        }





        /// <summary>
        /// 이벤트 발생
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="selectHandler"></param>
        public static void Publish(eGameEvent eventType, IEventBusParam selectHandler)
        {
            if (_events.TryGetValue(eventType, out var thisEvent))
            {
                thisEvent.Invoke(selectHandler);

            }
        }
    }
}
