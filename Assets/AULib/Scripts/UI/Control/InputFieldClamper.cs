using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using TMPro;
using System;

namespace AULib
{


    /// <summary>
    /// �ؽ�Ʈ �Է��ʵ� ���ڰ� Min, Max ���� ó��
    /// </summary>
    public class InputFieldClamper : BaseBehaviour
    {

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_InputField.ContentType _contentType;
        [SerializeField] private float _inputMinValue;
        [SerializeField] private float _inputMaxValue;


        public TMP_InputField Target => _inputField;


        private float _currentValue;
        public float CurrentValue => _currentValue;
        

        public event Action<float> onEndEdit;
        protected override void Awake() 
    	{
    		base.Awake();

            _inputField.contentType = _contentType;
            _inputField.onEndEdit.AddListener(HandleonEndEdit);
        }


        protected override void Start() 
    	{
    		base.Start();
		
		
    	}


        private void OnDestroy()
        {
            _inputField.onEndEdit.RemoveListener(HandleonEndEdit);
        }



        /// <summary>
        /// Min �� ����
        /// </summary>
        /// <param name="value"></param>
        public void SetMinValue(float value)
        {
            _inputMinValue = value;
        }

        /// <summary>
        /// Max �� ����
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxValue(float value)
        {
            _inputMaxValue = value;
        }

        /// <summary>
        /// �� ����
        /// </summary>
        public void SetValue(float value)
        {
            SetValue(value.ToString());
        }

        /// <summary>
        /// �� ����
        /// </summary>
        public void SetValue(string value)
        {
            HandleonEndEdit(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetIsEqualMinValue(float value)
        {
            return _inputMinValue == value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetIsEqualMaxValue(float value)
        {
            return _inputMaxValue == value;
        }


        private void HandleonEndEdit(string inputedString)
        {
            if (_contentType != TMP_InputField.ContentType.IntegerNumber && _contentType != TMP_InputField.ContentType.DecimalNumber)
            {
                //�Է����°� ���ڰ� �ƴϸ� ����
                Debug.LogWarning("�Է� �ʵ��� ContentType�� IntegerNumber �� DecimalNumber�� �ƴմϴ�.");
                return;
            }

            if (float.TryParse(inputedString, out var result)) {
                result = Mathf.Clamp(result, _inputMinValue, _inputMaxValue);
                _inputField.text = result.ToString();
                _currentValue = result;
                onEndEdit?.Invoke(result);
            }
        }
    }
}
