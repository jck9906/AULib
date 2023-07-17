using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AULib;
using Cysharp.Threading.Tasks;

namespace AULib
{ 
    public class ButtonSound : BaseBehaviour
    {

        //[SerializeField] AudioClip clip;
        [SerializeField] SoundEffectEnum clip;

        //����� ������ ��ũ��Ʈ�� �� �ʱ⿡ Ȱ��ȭ ��Ű�� ��찡 �־ �����̰� �ʿ���
        [SerializeField] float _delayAddToggle = 0.2f;

        private Button _targetButton;
        private Toggle _targetToggle;
        protected override void Awake()
        {
            base.Awake();
            _targetButton = GetComponent<Button>();
            _targetToggle = GetComponent<Toggle>();
        }

        void OnEnable()
        {
            if (_targetButton != null)
            {
                _targetButton.onClick.AddListener(PlayButtonSound);
            }

            AddToggleListener().Forget();
        }

        

        void OnDisable()
        {
            if (_targetButton != null)
            {
                _targetButton.onClick.RemoveListener(PlayButtonSound);
            }

            if (_targetToggle != null)
            {
                _targetToggle.onValueChanged.RemoveListener(PlayToggleSound);
            }
        }



        private async UniTaskVoid AddToggleListener()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayAddToggle));
            if (_targetToggle != null)
            {
                _targetToggle.onValueChanged.AddListener(PlayToggleSound);
            }
        }

        private void PlayToggleSound(bool isOn)
        {
            if(isOn) 
            {
                PlayButtonSound();
            }
        }

        void PlayButtonSound()
        {
            SoundManager.i.PlayAudio(clip);
        }
    }
}