using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AULib
{

    /// <summary>
    /// �� �⺻ ��Ʈ�ѷ� 
    /// �ε��� �Ŵ������� ȣ�� �� �ʱ�ȭ �Ѵ�.
    /// </summary>
    public abstract class BaseSceneController : BaseBehaviour, IBackable
    {
        [SerializeField] protected SceneLoadListener[] _sceneLoadListeners;
        [SerializeField] protected AssetPreLoader _assetPreLoader;

        //�ʱ�ȭ �Ϸ� ����
        protected bool _isDone = false;
        public bool IsDone => _isDone;

        //�ʱ�ȭ ���� ��Ȳ
        protected float _progress = 0f;
        public float Progress => _progress;

        //�ε��� �� �޼���
        protected string _loadingTipMessage;
        public string LoadingTipMessage => _loadingTipMessage;


        

        protected override void Awake()
        {
            base.Awake();

            if (_assetPreLoader == null) 
            {
                _assetPreLoader = GetComponent<AssetPreLoader>();
            }
                
            //1. �� �ε� �Ϸ�
            OnSceneLoaded();
        }




        /// <summary>
        /// �� �ʱ�ȭ ����
        /// </summary>
        /// <returns></returns>
        public async UniTaskVoid InitializeSceneAsync()
        {
            _loadingTipMessage = "�ּ� �ε� ��...";
            _progress += 0.2f;
            //2. �� �ʱ�ȭ ����            
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
           


            //����Ŭ���� �ʱ�ȭ ȣ��
            await InitializeGeneralSceneAsync();
            _progress += 0.2f;


            //�ʱ�ȭ �Ϸ�
            _isDone = true;
        }
      
        public void InitializeFinished()
        {
            //3. �� �ʱ�ȭ �Ϸ�
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
        /// ���� Ŭ���� �ʱ�ȭ ����
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