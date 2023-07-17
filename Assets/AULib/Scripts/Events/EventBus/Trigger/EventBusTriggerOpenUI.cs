using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using static AULib.EventBus;

namespace AULib
{
    public class EventBusTriggerOpenUI : BaseBehaviour
    {
        
        private void OnEnable()
        {
            EventBus.Publish(eGameEvent.OpenUI, new EventBusParamOpenUI(true, GetInstanceID()));
            
        }

        private void OnDisable()
        {
            EventBus.Publish(eGameEvent.OpenUI, new EventBusParamOpenUI(false, GetInstanceID()));
        }
    }
}
