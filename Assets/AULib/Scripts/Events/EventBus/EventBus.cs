using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEngine.Events;

namespace AULib
{
    /// <summary>
    /// ���� �̺�Ʈ ���� 
    /// </summary>
    public static class EventBus 
    {

        /// <summary>
        /// �̺�Ʈ Ÿ��
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
        /// �̺�Ʈ ����
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
        /// �̺�Ʈ ���� ����
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
        /// �̺�Ʈ �߻�
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
