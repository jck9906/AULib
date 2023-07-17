using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    [System.Serializable]

    public struct EventBusParamOpenUI : IEventBusParam
    {
        [SerializeField] private bool _isOpen;
        [SerializeField] private int _instanceID;


        public bool IsOpen => _isOpen;
        public int InstanceID => _instanceID;

        public EventBusParamOpenUI(bool isOpen, int instanceID)
        {
            _isOpen = isOpen;
            _instanceID = instanceID;
        }
    }
}
