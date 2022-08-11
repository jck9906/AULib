using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AULib.MessageBox;



namespace AULib
{

    /// <summary>
    /// ���ӿ��� �������� ���� UIó��
    /// </summary>
    public class CommonUIController : MonoSingleton<CommonUIController>
    {

        [SerializeField] private MessageBox _msgBox;
        [SerializeField] private WebLoading _webLoading;
        [SerializeField] private ToastMessage _toastMessage;



        public MessageBox MessageBox => _msgBox;
        public WebLoading WebLoading => _webLoading;
        public ToastMessage ToastMessage => _toastMessage;


        public override void Init()
        {
            _msgBox.Init();            
            _webLoading.Init();
            _toastMessage.Init();
        }

    }
}