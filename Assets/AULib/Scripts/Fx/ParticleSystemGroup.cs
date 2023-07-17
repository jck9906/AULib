using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using System;
using UnityEngine.Events;

namespace AULib
{

    /// <summary>
    /// 파티클 시스템 그룹으로 관리
    /// </summary>
    public class ParticleSystemGroup : BaseBehaviour
    {

        private ParticleSystem[] _particleSystems;
        [SerializeField] private bool _playOnAwake;



        #region Evnets
        public event Action onParticlePlay;
        public event Action onParticlePause;
        public event Action onParticleStop;
        #endregion

        protected override void Awake() 
    	{
    		base.Awake();


            SetGroup();
            PlayOnAwake();

        }

      



        /// <summary>
        /// Play
        /// </summary>
        public void Play()
        {
            foreach (var item in _particleSystems)
            {
                item.Play();
            }
            onParticlePlay?.Invoke();
        }

        /// <summary>
        /// Pause
        /// </summary>
        public void Pasue()
        {
            foreach (var item in _particleSystems)
            {
                item.Pause();
            }
            onParticlePause?.Invoke();
        }


        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            foreach (var item in _particleSystems)
            {
                item.Stop();
            }
            onParticleStop?.Invoke();
        }








        private void SetGroup()
        {
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
        }

      
        private void PlayOnAwake()
        {
            Stop();
            if (_playOnAwake)
            {
                Play();
            }
        }

    }
}
