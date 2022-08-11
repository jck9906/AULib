using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{

    /// <summary>
    /// 모노 비헤이비어 확장
    /// </summary>
    public class BaseBehaviour : MonoBehaviour
    {

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }





        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}
