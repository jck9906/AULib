using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class ConstantScaleController : BaseBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] float targetSize;

        void FixedUpdate()
        {
            if (target == null) return;

            float target_size = (target.transform.position - transform.position).magnitude * 0.001f * targetSize;
            transform.localScale = new Vector3(target_size, target_size, target_size);
        }
    }
}