using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using System;

namespace AULib
{
    [RequireComponent(typeof(ParticleSystemGroup))]
    public class ParticleSystemGroupSound : BaseBehaviour
    {

        [SerializeField] private SoundEffectEnum clip;

        private ParticleSystemGroup _particleSystemGroup;

        protected override void Awake() 
    	{
    		base.Awake();

            _particleSystemGroup = GetComponent<ParticleSystemGroup>();


            _particleSystemGroup.onParticlePlay += HandleOnParticlePlay;
        }

        private void OnDestroy()
        {
            _particleSystemGroup.onParticlePlay -= HandleOnParticlePlay;
        }

        private void HandleOnParticlePlay()
        {
            SoundManager.i.PlayAudio(clip);
        }
    }
}
