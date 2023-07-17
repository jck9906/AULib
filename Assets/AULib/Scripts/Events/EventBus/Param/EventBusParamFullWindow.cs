using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{
    [System.Serializable]

    public struct EventBusParamFullWindow : IEventBusParam
    {
        [SerializeField] private bool _isOn;
        public bool IsOn => _isOn;

        public EventBusParamFullWindow(bool isOn)
        {
            _isOn = isOn;
        }
    }
}
