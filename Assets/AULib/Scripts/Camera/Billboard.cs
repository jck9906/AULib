using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class Billboard : BaseBehaviour
    {
        [SerializeField] Vector3 vecRot = new Vector3(-90, 0, 0);
        [SerializeField] Transform lookTarget;
        [SerializeField] bool useCanvas = true;


        private Canvas _canvas;
        protected override void Start()
        {
            base.Start();
            if (lookTarget == null)
            {
                UpdateLookTarget();
            }

            if ( useCanvas )
            {
                _canvas = gameObject.GetComponent<Canvas>();
                if ( _canvas == null )
                    _canvas = gameObject.GetComponentInParent<Canvas>();
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
                if ( useCanvas )
                {
                    //        Vector3 vecRot = Vector3.left;
                    float dis = Vector3.Distance( transform.position, lookTarget.position );

                    if ( 2f > dis || dis > 20f )
                    {
                        if ( _canvas.enabled == true )
                        {
                            _canvas.enabled = false;
                        }
                        return;
                    }
                    else if ( _canvas.enabled == false )
                    {
                        _canvas.enabled = true;
                    }
                }
                transform.LookAt(transform.position + lookTarget.transform.rotation * Vector3.forward, lookTarget.transform.rotation * Vector3.up);
                transform.Rotate(vecRot);
            }
        }
    }

}
