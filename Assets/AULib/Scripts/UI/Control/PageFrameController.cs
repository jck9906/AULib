using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    [Serializable]
    public struct PageFrameViewInfo
    {
        public string Title;

        public string Prev;

        public bool EnablePrev;

        public string Next;

        public bool EnableNext;
    }


    public class PageFrameController : BaseBehaviour, ICommonUIElement
    {
        [SerializeField] TextMeshProUGUI _txtTitle;
        [SerializeField] TextMeshProUGUI _txtPrev;
        [SerializeField] TextMeshProUGUI _txtNext;
        [SerializeField] Button _btnPrev;
        [SerializeField] Button _btnNext;


        public static event Action onClickPrev;
        public static event Action onClickNext;







        private void OnEnable()
        {
            _btnPrev.onClick.AddListener(HandleOnClickPrev);
            _btnNext.onClick.AddListener(HandleOnClickNext);
        }

        private void OnDisable()
        {
            _btnPrev.onClick.RemoveListener(HandleOnClickPrev);
            _btnNext.onClick.RemoveListener(HandleOnClickNext);
        }


        #region public

        public void Init()
        {
            Hide();
        }

        /// <summary>
        /// 현재 단계 설정에 따른 UI 셋팅
        /// </summary>
        /// <param name="step"></param>
        public void SetView(PageFrameViewInfo info)
        {
            Show();
            _txtTitle.text = info.Title;

            _btnPrev.gameObject.SetActive(info.EnablePrev);

            if (info.EnablePrev)
            {
                _txtPrev.text = info.Prev;
                _btnPrev.interactable = info.EnablePrev;
            }


            _btnNext.gameObject.SetActive(info.EnableNext);
            if (info.EnableNext)
            {
                _txtNext.text = info.Next;
                _btnNext.interactable = info.EnableNext;
            }
        }

        /// <summary>
        /// 이전 버튼 액티브 셋팅
        /// </summary>
        /// <param name="active"></param>
        public void SetActivePrevBtn(bool active)
        {
            _btnPrev.gameObject.SetActive(active);
        }


        /// <summary>
        /// 다음 버튼 액티브 셋팅
        /// </summary>
        /// <param name="active"></param>
        public void SetActiveNextBtn(bool active)
        {
            _btnNext.gameObject.SetActive(active);
        }


        /// <summary>
        /// 이전 버튼 활성/비활성 셋팅
        /// </summary>
        /// <param name="interactive"></param>
        public void SetInteractivePrevBtn(bool interactive)
        {
            _btnPrev.interactable = interactive;
        }

        /// <summary>
        /// 다음 버튼 활성/비활성 셋팅
        /// </summary>
        /// <param name="interactive"></param>
        public void SetInteractiveNextBtn(bool interactive)
        {
            _btnNext.interactable = interactive;
        }
        #endregion public











        #region private & protected
        private void HandleOnClickPrev()
        {
            onClickPrev?.Invoke();
        }

        private void HandleOnClickNext()
        {
            onClickNext?.Invoke();
        }

        #endregion
    }
}
