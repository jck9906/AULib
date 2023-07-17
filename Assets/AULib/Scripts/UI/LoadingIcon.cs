using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using Cysharp.Threading.Tasks;
using System;

namespace AULib
{
    public class LoadingIcon : BaseBehaviour
    {
        protected override void Awake() 
    	{
    		base.Awake();
    	}

        public async void Show(UniTask hideTask)
        {
            base.Show();
            await hideTask;
            Hide();
        }





        private void Update()
        {
            Debug.Log("LoadingIcon Show");
        }
    }
}
