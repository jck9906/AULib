using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace AULib
{
    public abstract class ListSortOrder<T, K> : BaseBehaviour
    where T : class
    where K : Enum
    {
        [SerializeField] protected TMP_Dropdown dropdownSort;
        //[SerializeField] protected Text txtSort;
        [SerializeField] protected Toggle toggleOrder;

        [SerializeField] protected bool _isOrderAscending;
        [SerializeField] protected K _currentSortType;

        [SerializeField] protected int _defaultSelectIndex;

        [SerializeField] protected List<SortData<K>> _sortDatas;


        public bool IsOrderAscending => _isOrderAscending;
        public K CurrentSortType => _currentSortType;


        public delegate void OnChangedSortOrderDelegate(bool isOrderAscending, K currentSortType);
        public event OnChangedSortOrderDelegate onChangedSortOrder;






        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

            dropdownSort.ClearOptions();
            dropdownSort.AddOptions(GetDropdownOptionDatas());            
        }

        protected virtual void OnEnable()
        {
            dropdownSort.onValueChanged.AddListener(HandleOnValueChangedOption);
            toggleOrder.onValueChanged.AddListener(HandleOnValueChangedOrder);
        }

        protected virtual void OnDisable()
        {
            dropdownSort.onValueChanged.RemoveListener(HandleOnValueChangedOption);
            toggleOrder.onValueChanged.RemoveListener(HandleOnValueChangedOrder);
        }

        public List<T> GetOrderedList(List<T> dataList)
        {
            IEnumerable<T> orderedDataList;
            if (_isOrderAscending)
            {
                //orderedDataList = dataList.OrderBy(GetOrderCondition);
                orderedDataList = dataList.OrderBy(GetOrderCondition);
            }
            else
            {
                orderedDataList = dataList.OrderByDescending(GetOrderCondition);
            }

            IEnumerable<T> newList = orderedDataList.Select(data => data as T);
            return newList.ToList();
        }


        public List<T> GetOrderedList(Dictionary<int, T> infoTable)
        {
            var infoList = new List<T>(infoTable.Values);
            var orderedList = GetOrderedList(infoList);
            return orderedList;

        }




        private void HandleOnValueChangedOption(int optionIndex)
        {
            _defaultSelectIndex = optionIndex;
            _currentSortType = (K)Enum.Parse(typeof(K), optionIndex.ToString(), true);
            onChangedSortOrder?.Invoke(_isOrderAscending, _currentSortType);
        }


        protected void HandleOnValueChangedOrder(bool isOn)
        {
            _isOrderAscending = isOn;

            onChangedSortOrder?.Invoke(_isOrderAscending, _currentSortType);
        }





        //정렬 방식 셋팅
        protected abstract object GetOrderCondition(T cell);
        protected abstract List<TMP_Dropdown.OptionData> GetDropdownOptionDatas();


    }
}