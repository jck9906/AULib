using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AULib.MessageBox;



namespace AULib
{

    public interface ICommonUIElement
    {
        void Init();
        void Show();
        void Hide();
    }
    /// <summary>
    /// 게임에서 공통으로 쓰는 UI처리
    /// </summary>
    public class CommonUIController : BaseBehaviour
    {

        [SerializeField] private MessageBox _msgBox;
        [SerializeField] private WebLoading _webLoading;
        [SerializeField] private LoadingIcon _loadingIcon;
        [SerializeField] private ToastMessage _toastMessage;
        [SerializeField] private PageFrameController _pageFrame;



        public MessageBox MessageBox => _msgBox;
        public WebLoading WebLoading => _webLoading;
        public LoadingIcon LoadingIcon => _loadingIcon;
        public ToastMessage ToastMessage => _toastMessage;
        public PageFrameController PageFrame => _pageFrame;

        [SerializeField] private List<ICommonUIElement> _commonUIElements = new List<ICommonUIElement>();



        protected override void Awake()
        {
            _commonUIElements.Add(_msgBox);
            _commonUIElements.Add(_webLoading);
            _commonUIElements.Add(_toastMessage);
            _commonUIElements.Add(_pageFrame);


            InitAll();
        }
      


        /// <summary>
        /// 
        /// </summary>
        public void InitAll()
        {
            foreach (var item in _commonUIElements)
            {
                item.Init();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowAll()
        {
            foreach (var item in _commonUIElements)
            {
                item.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void HideAll()
        {
            foreach (var item in _commonUIElements)
            {
                item.Hide();
            }
        }

    }
}