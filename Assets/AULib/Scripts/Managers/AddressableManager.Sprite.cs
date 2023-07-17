using System;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace AULib
{
    /// <summary>
    /// AddressableManager - 스프라이트 관련 분리
    /// </summary>
    public static partial class AddressableManager
    {


        private static SpriteAtlasData spriteAtlasDatas;

        public static void SpriteAtlasInit()
        {
            LoadAssetAsync<SpriteAtlasData>(AULibSetting.ATLAS_LIST_DATA_PATH, handle =>
            {
                spriteAtlasDatas = handle.Result;
            });
        }

        /// <summary>
        /// AssetReference로 아틀라스에서 스프라이트 로드
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        public static AsyncOperationHandle LoadSpriteAtlasAsyncWithReference(AssetReferenceAtlasedSprite assetReference, Action<AsyncOperationHandle<Sprite>> OnLoaded = null)
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

        public static void LoadSpriteAsyncWithReference(AssetReferenceSprite assetReference, Action<Sprite> OnLoaded = null)
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
                    OnLoaded(handle.Result);
                }
                else
                {
                    // Removed OnLoaded in-case it's already been added.
                    handle.Completed -= (op) => { OnLoaded(op.Result); }; ;
                    handle.Completed += (op) => { OnLoaded(op.Result); }; ;
                }
            }
            else
            {

                handle = assetReference.LoadAssetAsync();

                // Removed OnLoaded in-case it's already been added.
                handle.Completed -= (op) => { OnLoaded(op.Result); }; ; ;
                handle.Completed += (op) => { OnLoaded(op.Result); }; ; ;
            }
        }


        public static void LoadSpriteAsync(string assetPath, Action<Sprite> OnLoaded = null)
        { 
#if UNITY_EDITOR
            if (AULibSetting.USE_ATLAS_ON_EDITOR)
            {
                LoadSpriteFromAtlas(assetPath, OnLoaded);               
            }
            else
            {
                LoadSpriteFromAssetPath(assetPath, OnLoaded);
            }
#else
            LoadSpriteFromAtlas(assetPath, OnLoaded);
#endif

        }

      


        //        public static AsyncOperationHandle<Sprite> LoadSpriteAsync(string assetPath , Action<AsyncOperationHandle<Sprite>> OnLoaded = null)
        //        {
        //            AsyncOperationHandle<Sprite> handle = default(AsyncOperationHandle<Sprite>);

        //            string address;
        //#if UNITY_EDITOR
        //            if (AULibSetting.USE_ATLAS_ON_EDITOR)
        //            {
        //                address = GetSpriteAddress(assetPath);
        //                //SpriteAtlas spriteAtlas;

        //                //spriteAtlas.GetSprite(address);
        //            }
        //            else
        //            {
        //                address = assetPath;
        //            }
        //#else
        //            address = GetSpriteAddress(assetPath);
        //#endif
        //            //address = assetPath;

        //            //애셋 로드
        //            handle = Addressables.LoadAssetAsync<Sprite>( address );



        //            var downloadStatus = handle.GetDownloadStatus();

        //            handle.Completed -= OnLoaded;
        //            handle.Completed += OnLoaded;

        //            return handle;
        //        }


        public static string GetSpriteAddress(string assetPath)
        {


            string atlasName = GetSpriteAtlasAdress(assetPath);
            string spriteName = GetSpriteName(assetPath);

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

        public static string GetSpriteAtlasAdress(string assetPath)
        {
            string atlasName = GetSpriteAtlasName(assetPath);            
            atlasName = new StringBuilder(atlasName).Append(".spriteatlas").ToString();
            
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(AULibSetting.ATLAS_CREATE_BUNDLE_PATH);
            strBuilder.Append("/");
            strBuilder.Append(atlasName);            
            string strBundleName = strBuilder.ToString();

            return strBundleName;
        }

        public static string GetSpriteName(string assetPath)
        {
            string spriteName = assetPath.Substring(assetPath.LastIndexOf('/') + 1);
            spriteName = spriteName.Substring(0, spriteName.IndexOf('.'));
            return spriteName;
        }






        private static void LoadSpriteFromAtlas(string assetPath, Action<Sprite> OnLoaded = null)
        {
            string atlasAddress = GetSpriteAtlasAdress(assetPath);
            string assetName = GetSpriteName(assetPath);

            //애셋 로드
            AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>(atlasAddress);
            handle.Completed -= (op) => { OnLoaded(op.Result.GetSprite(assetName)); };
            handle.Completed += (op) => { OnLoaded(op.Result.GetSprite(assetName)); };
        }

        private static void LoadSpriteFromAssetPath(string assetPath, Action<Sprite> OnLoaded = null)
        {


            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(assetPath);


            handle.Completed -= (op) => { OnLoaded(op.Result); };
            handle.Completed += (op) => { OnLoaded(op.Result); };
        }

        private static string GetSpriteAtlasName(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            
            string atlasPath = assetPath.Substring(0, assetPath.LastIndexOf('/'));
            string atlasName = atlasPath.Substring(atlasPath.LastIndexOf('/') + 1);
            if (!spriteAtlasDatas.IsContain(atlasName))
            {
                return GetSpriteAtlasName(atlasPath);
            }
            return atlasName;
        }
    }

}
