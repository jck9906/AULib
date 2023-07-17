using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using System;

namespace AULib
{
    /// <summary>
    /// 자동 회전 
    /// </summary>
    public class AutoRotate : BaseBehaviour
    {
        [SerializeField] private Transform _targetTrasnform;
        [SerializeField] private Vector3 _rotateAngle;
        [SerializeField] private float _speed;
        

        [SerializeField] private bool _isYoYo;
        //[NonSerialized][Range(0.1f, 5.0f)] public float Interval = 2.0f;
        [NonSerialized] public Vector3 DefaultAngle = new Vector3(0f, 45f, 0f);
        public bool IsYoYo => _isYoYo;

        private Vector3 _targetAngle;

        public Vector3 TargetAngle
        {
            get => _targetAngle;
            set => _targetAngle = value;
        }
        private float _targetTime;


        protected override void Awake()
        {
            base.Awake();

            _targetAngle = _targetTrasnform.eulerAngles;
            _targetTime = 0f;
        }

        private void Update()
        {
            
            if (_isYoYo)
            {
                _targetTime += Time.deltaTime;
                //float ang = (_targetTime % Interval) / Interval * Mathf.PI * _speed;
                float ang = (_targetTime)  * Mathf.PI * _speed;
                var t = Mathf.Sin(ang);
                _targetAngle = DefaultAngle + _rotateAngle * t ;
            }
            else
            {
                _targetTime = Time.deltaTime;
                _targetAngle += _rotateAngle * _targetTime * _speed;
                
            }

            _targetTrasnform.eulerAngles = _targetAngle;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetIsYoYo(bool isYoYo)
        {
            _isYoYo = isYoYo;
        }
    }
}
