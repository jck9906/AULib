using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AULib;
using System;
using UnityEngine.Events;

namespace AULib
{
    /// <summary>
    /// UI 슬라이더 확장
    /// 리셋버튼 추가
    /// </summary>
    public class SliderEx : BaseBehaviour
    {
        [Header("Component")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _btnReset;

        [Space()]
        [Header("Value")]
        [SerializeField] private float _maxValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _initValue;
        [SerializeField] private float _defaultValue;

        public float minValue { get { return _minValue; } }
        public float maxValue { get { return _maxValue; } }

        


        //[ReadOnly]
        [SerializeField] private float _currentValue;

        //public float Value => _currentValue;

        public float Value 
        {
            get { return _currentValue; }
            set { _currentValue = value; _slider.value = _currentValue; }
        }

        public bool IsFocusedObject
        {
            get
            {
                EventSystem eventSystem = EventSystem.current;
                if (eventSystem == null)
                {
                    return false;
                }

                GameObject selectedGameObject = eventSystem.currentSelectedGameObject;
                if(selectedGameObject == _slider.gameObject)
                {
                    return true;
                }
                return false;
            }
        }


        #region Events
        [Space()]
        public UnityEvent<float> onValueChanged;
        //public UnityEvent<float> onClickReset;
        #endregion
        #region Unity cylce
        protected override void Awake() 
    	{
    		base.Awake();

            _slider.maxValue = _maxValue;
            _slider.minValue = _minValue;

            _currentValue = _initValue;
        }


        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(HandleOnValueChangedSlider);
            _btnReset.onClick.AddListener(HandleOnClickReset);
            
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleOnValueChangedSlider);
            _btnReset.onClick.RemoveListener(HandleOnClickReset);
        }
        protected override void Start() 
    	{
    		base.Start();


            InitSliderValue();
        }

        #endregion


        public void SetInitValue(float initValue, float defulatValue = 0f)
        {
            _initValue = initValue;
            _defaultValue = defulatValue;            
            InitSliderValue();
        }



        private void InitSliderValue()
        {
            _slider.value = _initValue;
        }

        private void SetDefaultSliderValue()
        {
            _slider.value = _defaultValue;
        }

        #region Handler

        private void HandleOnValueChangedSlider(float value)
        {
            _currentValue = value;
            onValueChanged?.Invoke(value);
        }

        private void HandleOnClickReset()
        {
            SetDefaultSliderValue();
            //onClickReset?.Invoke(_initValue);
        }

        #endregion
    }
}
