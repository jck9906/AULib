using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// ����̽� Back ��ư �Է� ó��
    /// </summary>
    public class BackableHandler : BaseBehaviour
    {
        //public bool EnableBack = true;\

        //���� ���� ������Ʈ�� ���� ��� �ش� ������Ʈ ����
        //���� ���� ������Ʈ��� ��� ��
        [SerializeField] GameObject _backableObject;
        private IBackable _backable;


        protected override void Awake()
        {
            base.Awake();
            _backable = GetComponent<IBackable>();
            if (_backable == null)
            {
                _backable = _backableObject.GetComponent<IBackable>(); 
            }


            if (_backable == null)
            {
                Debug.LogError("IBackable ������Ʈ�� ã�� �� �����ϴ�. IBackable Ȯ�� ���θ� Ȯ���� �ּ���.");
            }
        }

        protected void OnEnable()
        {
            BackableHistoryManager.AddBackable(this);
        }

        protected void OnDisable()
        {
            BackableHistoryManager.RemoveBackable(this);
        }







        public void OnBackButtonInput()
        {
            _backable.OnBackButtonInput();
        }

    }
}