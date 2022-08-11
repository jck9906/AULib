using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AULib
{
    public class DestroyTimer : BaseBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] public float DestroyTime = 10;
        protected override void Start()
        {
            base.Start();
#if UNITASK
            DestroySelfAsync().Forget();
#else
            StartCoroutine(DestroySelf());
#endif
        }

#if UNITASK
        async UniTaskVoid DestroySelfAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DestroyTime), cancellationToken:this.GetCancellationTokenOnDestroy());            
            Destroy(gameObject);

            await UniTask.NextFrame();
        }
#else
        IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(DestroyTime);
            Destroy(gameObject);
            yield return null;
        }
#endif

    }
}