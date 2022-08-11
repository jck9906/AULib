using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AULib
{

    /// <summary>
    /// 토글 그룹 이벤트 처리 추가
    /// </summary>
    [RequireComponent(typeof(ToggleGroup))]
    public class ToggleGroupEx : BaseBehaviour
    {
        [Serializable]
        public class ToggleOption
        {
            public string label;
            public int index;
            public Toggle toggle;
        }
             
        [SerializeField] private List<ToggleOption> _toggleOptions;
        /*[SerializeField] */ToggleGroup _toggleGroup;


        private ToggleOption _currentToggleOption;
        public ToggleGroup toggleGroup => _toggleGroup;



        public event Action<int> OnChangedSelectToggle;


        protected override void Awake()
        {
            base.Awake();
            _toggleGroup = GetComponent<ToggleGroup>();

            if (_toggleOptions == null)
            {
                _toggleOptions = new List<ToggleOption>();
            }
        }

        protected override void Start()
        {
            base.Start();
            foreach (var item in _toggleOptions)
            {
                item.toggle.group = _toggleGroup;
            }
            SetToggleEvent();
        }

        /// <summary>
        /// 토글 등록
        /// </summary>
        /// <param name="toggle"></param>
        public void RegisterToggle(ToggleOption toggle)
        {
            _toggleGroup.RegisterToggle(toggle.toggle);
            _toggleOptions.Add(toggle);

            SetToggleEvent();
        }

        /// <summary>
        /// 토글 제거
        /// </summary>
        /// <param name="toggle"></param>
        public void UnregisterToggle(ToggleOption toggle)
        {
            _toggleGroup.UnregisterToggle(toggle.toggle);
            _toggleOptions.Remove(toggle);

            SetToggleEvent();
        }

        /// <summary>
        /// 토글 선택
        /// </summary>
        /// <param name="index"></param>
        public void SetSelectToggle(int index, bool isForceNotify = false)
        {
            var targetToggle = _toggleOptions.Find( tgl => tgl.index == index);
            if (targetToggle.toggle.isOn && isForceNotify)
            {
                OnChangedSelectToggle?.Invoke(targetToggle.index);
            }
            else
            {
                targetToggle.toggle.isOn = true;
            }
            
        }


        /// <summary>
        /// 토글 선택 해제
        /// </summary>
        public void SetSelectToggleNull()
        {
            foreach (ToggleOption item in _toggleOptions)
            {
                item.toggle.isOn = false;
            }
        }






        private void SetToggleEvent()
        {
            foreach (ToggleOption item in _toggleOptions)
            {
                item.toggle.onValueChanged.RemoveListener((isOn) =>
                {
                    HandleToggleSelected(item, isOn);
                });

                item.toggle.onValueChanged.AddListener( (isOn) =>
                {
                    HandleToggleSelected(item, isOn);
                });
            }
        }

        private void HandleToggleSelected(ToggleOption option, bool isOn)
        {
            if (isOn)
            {
                if (_currentToggleOption == option)
                {
                    return;
                }

                
                _currentToggleOption = option;

                OnChangedSelectToggle?.Invoke(_currentToggleOption.index);

            }
        }
    }

}

