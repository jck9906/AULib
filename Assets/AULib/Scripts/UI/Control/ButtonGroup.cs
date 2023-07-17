using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class ButtonGroup : BaseBehaviour
    {
        [Serializable]
        public class ButtonOption
        {
            public string label;
            public int index;
            public Button button;
        }






        [SerializeField] private List<ButtonOption> _buttonOptions;
        public List<ButtonOption> ButtonOptions => _buttonOptions;




        private ButtonOption _currentButtonOption;



        public event Action<int> OnClickButton;








        protected override void Awake()
        {
            base.Awake();
         
            if (_buttonOptions == null)
            {
                _buttonOptions = new List<ButtonOption>();
            }
        }



        protected override void Start()
        {
            base.Start();
            SetButtonEvent();
        }



        /// <summary>
        /// 버튼추가
        /// </summary>
        /// <param name="option"></param>
        public void AddButton(ButtonOption option)
        {
            _buttonOptions.Add(option);
            SetButtonEvent();
        }


        public void RemoveButton(ButtonOption option)
        {

        }


        public Button[] GetButtonObjects()
        {
            var buttons = _buttonOptions.Select(opt => opt.button).ToArray();
            return buttons;
        }








        private void SetButtonEvent()
        {
            foreach (ButtonOption item in _buttonOptions)
            {
                item.button.onClick.RemoveListener(() =>
                {
                    HandleOnClickButton(item);
                });

                item.button.onClick.AddListener(() =>
                {
                    HandleOnClickButton(item);
                });
            }
        }

        private void HandleOnClickButton(ButtonOption item)
        {
            _currentButtonOption = item;
            OnClickButton?.Invoke(_currentButtonOption.index);
        }
    }
}
