using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// 디바이스 Back 버튼 입력 처리
    /// </summary>
    public class BackableHandler : BaseBehaviour
    {
        //public bool EnableBack = true;\

        //같은 게임 오브젝트에 없을 경우 해당 오브젝트 연결
        //같은 게임 오브젝트라면 없어도 됨
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
                Debug.LogError("IBackable 오브젝트를 찾을 수 없습니다. IBackable 확장 여부를 확인해 주세요.");
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