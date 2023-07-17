using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;


namespace AULib
{
    public class WebLoading : BaseBehaviour, ICommonUIElement
    {
        private const float UPDATE_TIME = 0.1f;

        private bool requestMessage = false;
        private float keepTime = 0.5f;
        private bool keepClear = false;

        [SerializeField] private Image img;

        protected override  void Awake()
        {
            base.Awake();
            requestMessage = false;
            keepClear = false;
        }


        public void Init()
        {
            Hide();
        }


        public void WebRequest()
        {
            if (requestMessage == true)
                return;

            gameObject.SetActive(true);

            keepClear = false;
            requestMessage = true;
            WebRequestAsync().Forget();

        }

        public void WebResponse()
        {
            gameObject.SetActive(false);
            keepClear = false;
            requestMessage = false;
        }


        async UniTaskVoid WebRequestAsync()
        {
            float elapsed = 0.0f;

            while (requestMessage == true && keepClear)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(UPDATE_TIME), cancellationToken: this.GetCancellationTokenOnDestroy());

                img.transform.Rotate(new Vector3(0, 0, -0.1f));

                if (elapsed > keepTime)
                    keepClear = true;
                elapsed += UPDATE_TIME;
            }
        }



        public bool IsLoading() { return !keepClear; }
    }

}
