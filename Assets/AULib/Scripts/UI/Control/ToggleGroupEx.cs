using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AULib
{

    public delegate void ToggleChangedDelegate(ToggleGroupEx.ToggleOption option);
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

        /// <summary>
        /// 토글 아이템 보유 여부
        /// </summary>
        public bool IsToggleChildExist => _toggleOptions.Count > 0;

        public event ToggleChangedDelegate OnChangedSelectToggle;


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
            toggle.toggle.group = _toggleGroup;

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
        /// 
        /// </summary>
        public void RemoveAll(bool destroyInstance)
        {
            foreach (ToggleOption item in _toggleOptions)
            {
                //UnregisterToggle(item);
                _toggleGroup.UnregisterToggle(item.toggle);
                if (destroyInstance)
                {
                    Destroy(item.toggle.gameObject);
                }
            }
            _toggleOptions.Clear();
            SetToggleEvent();
        }

        /// <summary>
        /// 토글 선택
        /// </summary>
        /// <param name="index"></param>
        public void SetSelectToggle(int index, bool isForceNotify = false)
        {
            var targetToggle = _toggleOptions.Find( tgl => tgl.index == index);
            SetSelectToggle(targetToggle, isForceNotify);            
        }

        /// <summary>
        /// 토글 선택 - Notify 없음
        /// </summary>
        /// <param name="index"></param>
        public void SetSelectToggleWithoutNotfiy(int index)
        {
            var targetToggle = _toggleOptions.Find(tgl => tgl.index == index);
            if (targetToggle == null) return;
            targetToggle.toggle.SetIsOnWithoutNotify(true);
            targetToggle.toggle.GetComponent<ToggleEx>()?.SetToggleViewOn(true);
            SetCurrentToggleOption(targetToggle, false);
        }

        public void SetSelectFirstToggle(bool isForceNotify = false)
        {
            var targetToggle = _toggleOptions[0];
            SetSelectToggle(targetToggle, isForceNotify);
        }

        public void SetSelectToggle(ToggleOption toggleOption, bool isForceNotify)
        {
            if (toggleOption == null) return;
            if (toggleOption.toggle.isOn && isForceNotify)
            {
                OnChangedSelectToggle?.Invoke(toggleOption);
            }
            else
            {
                toggleOption.toggle.isOn = true;
            }
        }
        /// <summary>
        /// 토글 선택 해제
        /// </summary>
        public void SetSelectToggleNull()
        {
            _toggleGroup.SetAllTogglesOff();
            //foreach (ToggleOption item in _toggleOptions)
            //{
            //    item.toggle.isOn = false;
            //}
        }

        /// <summary>
        /// 토글 인덱스에 해당 하는 토글정보 리턴
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ToggleOption GetToggleOptionWithIndex(int index)
        {
            return _toggleOptions.Find(info => info.index.Equals(index));
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

                SetCurrentToggleOption(option, true);
            }

            if (_toggleGroup.allowSwitchOff)
            {
                if (!_toggleGroup.AnyTogglesOn())
                {
                    if (_currentToggleOption == option)
                        //if (_currentToggleOption.Equals(option))
                    {
                        SetCurrentToggleOption(null, true);
                    }
                }
            }
            
            
        }

        private void SetCurrentToggleOption(ToggleOption option, bool isNotify)
        {
            _currentToggleOption = option;
            if (isNotify)
            {
                OnChangedSelectToggle?.Invoke(_currentToggleOption);
            }
            
        }
    }

}

