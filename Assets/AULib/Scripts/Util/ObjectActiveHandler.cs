using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// ������ ������Ʈ Active ó��
    /// </summary>
    public class ObjectActiveHandler : MonoBehaviour
    {
        [SerializeField] GameObject[] _handleTargets;






        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            foreach (GameObject item in _handleTargets)
            {
                item.SetActive(active);
            }
        }
    }
}
