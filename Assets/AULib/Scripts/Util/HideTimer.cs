using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AULib
{
    public class HideTimer : MonoBehaviour
    {
        public void StartHide(float time)
        {
            gameObject.SetActive(true);
#if UNITASK
            HideSelfAsync(time).Forget();
#else
            StartCoroutine(HideSelf(time));
#endif

        }

#if UNITASK
        async UniTaskVoid HideSelfAsync(float time)
        {            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: this.GetCancellationTokenOnDestroy());
            gameObject.SetActive(false);
            await UniTask.NextFrame();            
        }
#else
        IEnumerator HideSelf(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
            yield return null;
        }
#endif

    }
}
