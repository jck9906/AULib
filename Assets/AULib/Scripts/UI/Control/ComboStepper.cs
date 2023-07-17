using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AULib;
using UnityEngine.UI;
using System;

namespace AULib
{

    /// <summary>
    /// 
    /// </summary>
    public class ComboStepper : BaseBehaviour
    {
        [SerializeField] Button _btnDecrease;
        [SerializeField] Button _btnIncrease;

        [SerializeField] int _defaultValue;
        [SerializeField] bool _isEnableInputEdit;
        [SerializeField] InputFieldClamper _inputField;

        private int _currentValue;
        public int CurrentValue
        {
            get => _currentValue;
            set => SetCurrentValue(value);
        }


        #region events
        public event Action<int> onChangedValue;
        #endregion

        protected override void Awake() 
    	{
    		base.Awake();


            _inputField.Target.interactable = _isEnableInputEdit;

        }


        private void OnEnable()
        {
            _btnDecrease.onClick.AddListener(HandleOnClickDecrease);
            _btnIncrease.onClick.AddListener(HandleOnClickIncrease);

            _inputField.onEndEdit += HandleOnEndEdit;
        }

      
        private void OnDisable()
        {
            _btnDecrease.onClick.RemoveListener(HandleOnClickDecrease);
            _btnIncrease.onClick.RemoveListener(HandleOnClickIncrease);

            _inputField.onEndEdit -= HandleOnEndEdit;
        }

        protected override void Start() 
    	{
    		base.Start();

            SetCurrentValue(_defaultValue);
        }












        private void SetCurrentValue(int value)
        {
            _inputField.SetValue(value);
        }
       
        private void HandleOnClickDecrease()
        {
            float newValue = _inputField.CurrentValue - 1;
            _inputField.SetValue(newValue);
        }

        private void HandleOnClickIncrease()
        {
            float newValue = _inputField.CurrentValue + 1;
            _inputField.SetValue(newValue);
        }

        private void HandleOnEndEdit(float value)
        {
            _currentValue = (int)value;

            HandleOnChangedCurrentValue();
            
        }

        private void HandleOnChangedCurrentValue()
        {
            bool enableDecrease = !_inputField.GetIsEqualMinValue(_currentValue);
            bool enableIncrease = !_inputField.GetIsEqualMaxValue(_currentValue);

            _btnDecrease.interactable = enableDecrease;
            _btnIncrease.interactable = enableIncrease;
            
            onChangedValue?.Invoke(_currentValue);
        }

    }
}
