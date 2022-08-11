using System;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AULib
{
    /// <summary>
    /// AddressableManager - 스프라이트 관련 분리
    /// </summary>
    public static partial class AddressableManager
    {

        /// <summary>
        /// AssetReference로 아틀라스에서 스프라이트 로드
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        public static AsyncOperationHandle LoadSpriteAsyncWithReference(AssetReferenceAtlasedSprite assetReference, Action<AsyncOperationHandle<Sprite>> OnLoaded = null)
        {
            AsyncOperationHandle op = assetReference.OperationHandle;
            AsyncOperationHandle<Sprite> handle = default(AsyncOperationHandle<Sprite>);

            if (assetReference.IsValid() && op.IsValid())
            {
                // Increase the usage counter & Convert.
                Addressables.ResourceManager.Acquire(op);
                handle = op.Convert<Sprite>();

                if (handle.IsDone)
                {
                    OnLoaded(handle);
                }
                else
                {
                    // Removed OnLoaded in-case it's already been added.
                    handle.Completed -= OnLoaded;
                    handle.Completed += OnLoaded;
                }
            }
            else
            {

                handle = assetReference.LoadAssetAsync();

                // Removed OnLoaded in-case it's already been added.
                handle.Completed -= OnLoaded;
                handle.Completed += OnLoaded;
            }

            return handle;
        }






        public static AsyncOperationHandle<Sprite> LoadSpriteAsync(string bundleName, string assetPath, Action<AsyncOperationHandle<Sprite>> OnLoaded = null)
        {
           
            string strBundleName;
#if UNITY_EDITOR
            if (AULibSetting.USE_ATLAS_ON_EDITOR)
            {
                strBundleName = GetSpriteAddress(assetPath);
            }
            else
            {
                strBundleName = GetAddress(bundleName, assetPath);
            }


#else
            strBundleName = GetSpriteAddress(assetPath);
#endif
            return LoadSpriteAsync(strBundleName, OnLoaded);
        }

        public static AsyncOperationHandle<Sprite> LoadSpriteAsync(string address, Action<AsyncOperationHandle<Sprite>> OnLoaded = null)
        {
            AsyncOperationHandle<Sprite> handle = default(AsyncOperationHandle<Sprite>);

            //애셋 로드
            handle = Addressables.LoadAssetAsync<Sprite>(address);



            var downloadStatus = handle.GetDownloadStatus();
            
            handle.Completed -= OnLoaded;
            handle.Completed += OnLoaded;

            return handle;
        }


        public static string GetSpriteAddress(string assetPath)
        {
            string atlasName = assetPath.Substring(0, assetPath.LastIndexOf('/'));
            atlasName = atlasName.Substring(atlasName.LastIndexOf('/') + 1);
            atlasName = new StringBuilder(atlasName).Append(".spriteatlas").ToString();

            string spriteName = assetPath.Substring(assetPath.LastIndexOf('/') + 1);
            spriteName = spriteName.Substring(0, spriteName.IndexOf('.'));

            string atlasPath = AULibSetting.ATLAS_PATH;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(atlasPath);
            strBuilder.Append("/");
            strBuilder.Append(atlasName);
            strBuilder.Append('[');
            strBuilder.Append(spriteName);
            strBuilder.Append(']');
            string strBundleName = strBuilder.ToString();

            return strBundleName;
        }
    }

}
