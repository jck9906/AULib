using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    /// <summary>
    /// 여러개 오브젝트 Active 처리
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
