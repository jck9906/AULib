using System;
using UnityEngine;

namespace AULib
{

    public interface ISortData 
    {
        int GetIndex();
        string GetName();

    }
    [System.Serializable]
    public class SortData<T> : ISortData where T : Enum
    {
        [SerializeField] private T _currentSortType;
        [SerializeField] private int _index;
        [SerializeField] private string _name;
        
        public SortData(T currentSortType, int index, string name)
        {
            _currentSortType = currentSortType;
            _index = index;
            _name = name;
        }

        public int GetIndex()
        {
            return _index;
        }

        public string GetName()
        {
            return _name;
        }

        public T GetSortType()
        {
            return _currentSortType;
        }
    }



}