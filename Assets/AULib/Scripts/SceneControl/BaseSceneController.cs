using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AULib
{

    /// <summary>
    /// 씬 기본 컨트롤러 
    /// 로딩씬 매니저에서 호출 후 초기화 한다.
    /// </summary>
    public abstract class BaseSceneController : BaseBehaviour, IBackable
    {
        [SerializeField] protected SceneLoadListener[] _sceneLoadListeners;
        [SerializeField] protected AssetPreLoader _assetPreLoader;

        //초기화 완료 여부
        protected bool _isDone = false;
        public bool IsDone => _isDone;

        //초기화 진행 상황
        protected float _progress = 0f;
        public float Progress => _progress;

        //로딩시 팁 메세지
        protected string _loadingTipMessage;
        public string LoadingTipMessage => _loadingTipMessage;


        

        protected override void Awake()
        {
            base.Awake();

            if (_assetPreLoader == null) 
            {
                _assetPreLoader = GetComponent<AssetPreLoader>();
            }
                
            //1. 씬 로딩 완료
            OnSceneLoaded();
        }




        /// <summary>
        /// 씬 초기화 시작
        /// </summary>
        /// <returns></returns>
        public async UniTaskVoid InitializeSceneAsync()
        {
            _loadingTipMessage = "애셋 로드 중...";
            _progress += 0.2f;
            //2. 씬 초기화 시작            
            await _assetPreLoader.LoadAsync();
            //Dummy code            
            _loadingTipMessage = "Scene is initializing #1...";
            _progress += 0.2f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _loadingTipMessage = "Scene is initializing #2...";
            _progress += 0.2f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _loadingTipMessage = "Scene is initializing #3...";
            _progress += 0.2f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
           


            //서브클래스 초기화 호출
            await InitializeGeneralSceneAsync();
            _progress += 0.2f;


            //초기화 완료
            _isDone = true;
        }
      
        public void InitializeFinished()
        {
            //3. 씬 초기화 완료
            if (_sceneLoadListeners != null)
            {
                foreach (var item in _sceneLoadListeners)
                {
                    item.OnSceneLoad();
                }
            }
            
            OnInitializeFinished();
        }




        protected virtual void OnSceneLoaded()
        {
#if UNITY_EDITOR
            if (!GameController.IsValid())
            {
                GameController.InstantiateGameController();
                LoadingSceneController.SceneLoadCallback(this);
                return;
            }

#endif
            LoadingSceneController.SceneLoadCallback(this);
            //UIManager.i.Common.HideAll();
            UIManager.i.Common.HideAll();
        }










        #region Implements IBackable
        public virtual void OnBackButtonInput()
        {
            
        }
        #endregion Implements IBackable


        #region virtual & abstract

        /// <summary>
        /// 서브 클래스 초기화 진행
        /// </summary>
        /// <returns></returns>
        protected virtual async UniTask InitializeGeneralSceneAsync()
        {
            await UniTask.Yield();
        }

        protected abstract void OnInitializeFinished();

        #endregion
    }
}