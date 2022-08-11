using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AULib;

namespace AULib
{ 
    public class ButtonSound : BaseBehaviour
    {

        //[SerializeField] AudioClip clip;
        [SerializeField] SoundEffectEnum clip;

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

            if (_targetToggle != null)
            {
                _targetToggle.onValueChanged.AddListener(PlayToggleSound);
            }
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