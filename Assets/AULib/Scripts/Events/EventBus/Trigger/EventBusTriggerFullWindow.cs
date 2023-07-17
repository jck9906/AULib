using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using static AULib.EventBus;

namespace AULib
{
    public class EventBusTriggerFullWindow : BaseBehaviour
    {
        
        private void OnEnable()
        {
            EventBus.Publish(eGameEvent.FullWindow, new EventBusParamFullWindow(true));
        }

        private void OnDisable()
        {
            EventBus.Publish(eGameEvent.FullWindow, new EventBusParamFullWindow(false));
        }
    }
}
