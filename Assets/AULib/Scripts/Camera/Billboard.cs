using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class Billboard : BaseBehaviour
    {
        [SerializeField] Vector3 vecRot = new Vector3(-90, 0, 0);
        [SerializeField] Transform lookTarget;


        protected override void Start()
        {
            base.Start();
            if (lookTarget == null)
            {
                UpdateLookTarget();
            }
        }

        void UpdateLookTarget()
        {
            if (Camera.main != null)
            {
                lookTarget = Camera.main.transform;
            }
        }

        void LateUpdate()
        {
            if (lookTarget == null)
            {
                UpdateLookTarget();
            }
            else
            {
                //        Vector3 vecRot = Vector3.left;
                transform.LookAt(transform.position + lookTarget.transform.rotation * Vector3.forward, lookTarget.transform.rotation * Vector3.up);
                transform.Rotate(vecRot);
            }
        }
    }

}
