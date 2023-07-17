using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class AccordionItem : BaseBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private float  _minHeight;





        protected override void Awake()
        {
            base.Awake();
            
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(HandleOnToggleChanged);
        }

        protected void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(HandleOnToggleChanged);
        }

        private void HandleOnToggleChanged(bool state)
        {
            if (state)
            {
                _layoutElement.preferredHeight = -1f;
            }
            else
            {
                _layoutElement.preferredHeight = _minHeight;
            }
        }
    }
}
