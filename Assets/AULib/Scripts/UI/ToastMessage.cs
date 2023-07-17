using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AULib
{
    /// <summary>
    /// Toast �޼���
    /// </summary>
    public class ToastMessage : BaseBehaviour, IPointerDownHandler, ICommonUIElement
    {


        
        public const float SHOW_DURATION_SHORT = 1f;
        public const float SHOW_DURATION_NORMAL = 3f;
        public const float SHOW_DURATION_LONG = 5f;

        
        [SerializeField] private RectTransform rectTr;
        [SerializeField] private TMP_Text txtMessage;

        [SerializeField] private float tweenDuration = 0.5f;

        [SerializeField] private float _hidePositionY;
        [SerializeField] private float _showPositionY;

        private float _showDuration;

        private CancellationTokenSource _cancellationTokenSource;

        public void Init()
        {
            Hide(false);
            _showDuration = SHOW_DURATION_NORMAL;
        }





        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ToastMessage SetMessage(string message)
        {
            txtMessage.text = message;            
            Show(true);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public ToastMessage SetDuration(float duration)
        {
            _showDuration = duration;            
            return this;
        }

        /// <summary>
        /// ���ڽ� ���� ����
        /// </summary>
        public ToastMessage SetBackgroundColor(Color color)
        {
            //TODO : ���ڽ� ���� ����

            return this;
        }

        /// <summary>
        /// �ؽ�Ʈ ���� ����
        /// </summary>
        public ToastMessage SetTextColor(Color color)
        {
            //TODO : �ؽ�Ʈ ���� ����

            return this;
        }


        private void Show(bool isTween)
        {
            Hide(false);
            gameObject.SetActive(true);
            if (isTween)
            {
                rectTr.DOAnchorPosY(_showPositionY, tweenDuration).OnComplete(HandleOnCompleteShow);
            }
            else
            {
                rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x, _showPositionY);
            }
            
            
        }

     

        private void Hide(bool isTween)
        {
            _cancellationTokenSource?.Cancel();
            if (isTween)
            {
                rectTr.DOAnchorPosY(_hidePositionY, tweenDuration).OnComplete( () => gameObject.SetActive(false));
            }
            else
            {
                rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x, _hidePositionY);
                gameObject.SetActive(false);
            }
        }

        private async UniTaskVoid HideAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_showDuration), ignoreTimeScale: false, cancellationToken: _cancellationTokenSource.Token);
            Hide(true);
        }


        private void HandleOnCompleteShow()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            HideAsync().Forget();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Hide(false);
        }
    }

}
