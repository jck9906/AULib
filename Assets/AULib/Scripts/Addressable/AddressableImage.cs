using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System.Security.Policy;
using System;
using UnityEditor;

namespace AULib
{

    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class AddressableImage : BaseBehaviour
    {

        [SerializeField] private AddressableSprite _sprite;


        protected override void Awake() 
    	{
    		base.Awake();

            
    	}

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (_sprite != null)
                SetTargetSprite(_sprite.spriteReference.editorAsset as Sprite);

#endif
        }

        protected override void Start() 
    	{
    		base.Start();
#if !UNITY_EDITOR
            LoadSprite();
#endif
        }






        private void LoadSprite()
        {
            _sprite.GetSprite(SetTargetSprite);            
        }

        private void SetTargetSprite(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }
}
