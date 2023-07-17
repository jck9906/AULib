using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    /// <summary>
    /// 업/다운 버튼이 있는 스크롤바 처리
    /// </summary>
    public class ScrollbarVerticalWithButtons : BaseBehaviour
    {
        private Scrollbar _scrollbar;
        [SerializeField] private TransformChildrenChangeTrigger _transformChildrenChangeTrigger;
        [SerializeField] private Button _btnUp;
        [SerializeField] private Button _btnDown;

        protected override void Awake()
        {
            base.Awake();
            _scrollbar = GetComponent<Scrollbar>();
            _transformChildrenChangeTrigger.onTransformChildrenChanged += HandleOnScrollElementChanged;
        }

        protected override void Start()
        {
            base.Start();
        }


        private void OnEnable()
        {
            _scrollbar.onValueChanged.AddListener(HandleOnValueChangedScroll);

            
            _btnUp.onClick.AddListener(HandleOnClickScrollUp);
            _btnDown.onClick.AddListener(HandleOnClickScrollDown);
        }

        private void OnDisable()
        {
            _scrollbar.onValueChanged.RemoveListener(HandleOnValueChangedScroll);

            
            _btnUp.onClick.RemoveListener(HandleOnClickScrollUp);
            _btnDown.onClick.RemoveListener(HandleOnClickScrollDown);
        }

        private void OnDestroy()
        {
            _transformChildrenChangeTrigger.onTransformChildrenChanged -= HandleOnScrollElementChanged;
        }


        private void HandleOnScrollElementChanged(Transform obj)
        {
            HandleOnValueChangedScroll(1);
        }

        private void HandleOnValueChangedScroll(float value)
        {
            _btnUp.interactable = true;
            _btnDown.interactable = true;

            if (value >= 1)
            {
                _btnUp.interactable = false;
            }

            if (value <= 0)
            {
                _btnDown.interactable = false;
            }
        }

        private void HandleOnClickScrollUp()
        {
            float targetValue = Mathf.Clamp01(_scrollbar.value + 0.1f);
            _scrollbar.value = targetValue;
        }

        private void HandleOnClickScrollDown()
        {
            float targetValue = Mathf.Clamp01(_scrollbar.value - 0.1f);
            _scrollbar.value = targetValue;
        }

    }
}
