using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class TransformChildrenChangeTrigger : MonoBehaviour
    {
        public event Action<Transform> onTransformChildrenChanged;

        private void OnTransformChildrenChanged()
        {
            onTransformChildrenChanged?.Invoke(transform);
        }
    }
}
