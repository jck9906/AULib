using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEngine.AddressableAssets;
using System;
using Cysharp.Threading.Tasks;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace AULib
{
    /// <summary>
    /// ��巹���� �ּ� �����δ�
    /// </summary>
    public class AssetPreLoader : BaseBehaviour
    {

        [SerializeField] private AssetPreloadTable _assetPreLoadTable;


        protected override void Awake() 
    	{
    		base.Awake();
		
    	}
	
    	/// <summary>
        /// �ּ� ���� �ε�
        /// </summary>
        /// <param name="onPreLoadComplete"></param>
        public void Load(Action<bool> onPreLoadComplete)
        {
#if UNITY_EDITOR
            onPreLoadComplete?.Invoke(true);
#else
            AddressableManager.LoadAssetsAsync<UnityEngine.Object>(_assetPreLoadTable.GetKeys(), handle =>
            {
                onPreLoadComplete?.Invoke(handle.IsValid());
            });
#endif

        }


        /// <summary>
        /// �ּ� �񵿱� �ε�
        /// </summary>
        /// <returns></returns>
        public async UniTask LoadAsync()
        {
#if UNITY_EDITOR
            await UniTask.Yield();
#else
            var handle = AddressableManager.LoadAssetsAsync<UnityEngine.Object>(_assetPreLoadTable.GetKeys());
            await handle;
#endif
        }
    }
}
