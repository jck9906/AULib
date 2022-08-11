using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{

    /// <summary>
    /// ��� �����̺�� Ȯ��
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
