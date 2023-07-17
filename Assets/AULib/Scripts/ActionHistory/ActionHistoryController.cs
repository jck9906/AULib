using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    /// <summary>
    /// Undo/Redo 컨트롤러 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionHistoryController<T> : BaseBehaviour where T : IHistoryable
    {

        [SerializeField] private Button btnReset;
        [SerializeField] private Button btnUndo;
        [SerializeField] private Button btnRedo;


        [SerializeField] private ActionHistoryManager<T> actionHistoryManager;


        public event Action<T> OnChangeHistory;
        //public event Action<T> OnUndo;
        //public event Action<T> OnRedo;





        protected override void Awake()
        {
            base.Awake();
            
            actionHistoryManager = new ActionHistoryManager<T>();

            Init();

            SetButtonInteraction();
        }

        private void OnEnable()
        {
            btnReset.onClick.AddListener(HandleOnClickReset);
            btnUndo.onClick.AddListener(HandleOnClickUndo);
            btnRedo.onClick.AddListener(HandleOnClickRedo);
        }



        private void OnDisable()
        {
            btnReset.onClick.RemoveListener(HandleOnClickReset);
            btnUndo.onClick.RemoveListener(HandleOnClickUndo);
            btnRedo.onClick.RemoveListener(HandleOnClickRedo);
        }






        /// <summary>
        /// 초기화 처리
        /// </summary>
        public void Init()
        {
            actionHistoryManager.Init();
        }

        /// <summary>
        /// 초기화 기본 정보 셋팅
        /// </summary>
        /// <param name="actionInfo"></param>
        public void SetInitInfo(T actionInfo)
        {
            actionHistoryManager.SetInitInfo(actionInfo);
        }

        /// <summary>
        /// 히스토리 추가
        /// </summary>
        /// <param name="actionInfo"></param>
        public void AddAction(T actionInfo)
        {
            actionHistoryManager.AddAction(actionInfo);
            SetButtonInteraction();
        }














        private void SetButtonInteraction()
        {
            btnUndo.interactable = IsUndo();
            btnRedo.interactable = IsRedo();
        }


        private T ActionUndo()
        {
            T historyCommand = actionHistoryManager.ActionUndo();
            SetButtonInteraction();
            return historyCommand;
        }

       
        private T ActionRedo()
        {
            T historyCommand = actionHistoryManager.ActionRedo();
            SetButtonInteraction();
            return historyCommand;
        }


        private bool IsUndo()
        {
            return actionHistoryManager.IsUndo();
        }
        private bool IsRedo()
        {
            return actionHistoryManager.IsRedo();
        }

        private void HandleOnClickReset()
        {
            Init();
            SetButtonInteraction();
            OnChangeHistory?.Invoke(actionHistoryManager.EmptyInfo);
        }

        private void HandleOnClickUndo()
        {
            T historyCommand = ActionUndo();
            OnChangeHistory?.Invoke(historyCommand);
        }

        private void HandleOnClickRedo()
        {
            T historyCommand = ActionRedo();
            OnChangeHistory?.Invoke(historyCommand);
        }
    }
}
