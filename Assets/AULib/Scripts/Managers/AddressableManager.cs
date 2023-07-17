using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Threading.Tasks;

namespace AULib
{
    public static partial class AddressableManager
    {

        // 에셋번들 관리용 Dic -> 내부적으로 관리되고 있어서 캐싱 필요 없음
        //static Dictionary<string, UnityEngine.Object> _loadedAssetBundles;


        //사용한 핸들 관리용 Dic 
        private static Dictionary<string, AsyncOperationHandle> loadedHandles = new();

        /// <summary>
        /// 초기화 처리
        /// </summary>
        /// <param name="onInit"></param>
        public static void Init(Action onInit = null)
        {
            loadedHandles = new Dictionary<string, AsyncOperationHandle>();
            var async = Addressables.InitializeAsync();
            async.Completed += (op) =>
            {
                onInit?.Invoke();
                Addressables.Release(async);
            };

            
        }

        /// <summary>
        /// 카탈로그 업데이트 체크 및 처리
        /// </summary>
        /// <param name="onUpdate"></param>
        public static void UpdateCatalogsAsync(Action<bool/*, List<string>*/> onUpdate = null)
        {

            List<string> catalogsToUpdate = new List<string>();
            AsyncOperationHandle<List<string>> checkForUpdateHandle = Addressables.CheckForCatalogUpdates(autoReleaseHandle:true);
            checkForUpdateHandle.Completed += opCheck =>
            {
                catalogsToUpdate.AddRange(opCheck.Result);

                if (catalogsToUpdate.Count > 0)
                {
                    AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate, autoReleaseHandle:true);

                    updateHandle.Completed += opUpdate =>
                    {
                        onUpdate?.Invoke(true/*, updateHandle.Result[0].Keys.Select(key => key.ToString()).ToList()*/);
                    };
                }
                else
                {
                    onUpdate?.Invoke(false/*, null*/);
                }
            };
        }

        /// <summary>
        /// 프리로딩 데이터
        /// </summary>
        /// <param name="keys"></param>
        public static void PreloadingDownload(List<string> keys, Action<bool> OnDownload)
        {
            HandlePreloadingDownload(keys, OnDownload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="OnDownload"></param>
        public static void PreloadingDownload(List<AssetLabelReference> keys, Action<bool> OnDownload)
        {
            HandlePreloadingDownload(keys, OnDownload);
        }


        /// <summary>
        /// 다운로드할 번들 용량 체크
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="OnGetDownloadSize"></param>
        public static void GetDownloadSize(List<string> keys, Action<long> OnGetDownloadSize)
        {
            HandleGetDownloadSize(keys, OnGetDownloadSize);
        }

        /// <summary>
        /// 다운로드할 번들 용량 체크
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="OnGetDownloadSize"></param>
        public static void GetDownloadSize(List<AssetLabelReference> keys, Action<long> OnGetDownloadSize)
        {
            HandleGetDownloadSize(keys, OnGetDownloadSize);
        }

      
        /// <summary>
        /// 번들 다운로드
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="onCompleteDownload"></param>
        public static async UniTaskVoid DownloadDependenciesAsync(List<string> keys, Action<bool> onCompleteDownload, Action<float> onProgressDownload)
        {
            await HandleDownloadDependenciesAsync(keys, onCompleteDownload, onProgressDownload);
        }


        public static async UniTaskVoid DownloadDependenciesAsync(List<AssetLabelReference> keys, Action<bool> onCompleteDownload, Action<float> onProgressDownload)
        {
            await HandleDownloadDependenciesAsync(keys, onCompleteDownload, onProgressDownload);
        }

        /// <summary>
        /// Address로 애셋 로드
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="bundleName"></param>
        /// <param name="assetPath"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        //public static AsyncOperationHandle<TObject> LoadAssetAsync<TObject>( string assetPath, Action<AsyncOperationHandle<TObject>> OnLoaded = null)
        //{
        //    // string strBundleName = GetAddress(bundleName, assetPath);
        //    return LoadAssetAsync<TObject>(assetPath, OnLoaded);
        //}

        public static AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(string address, Action<AsyncOperationHandle<TObject>> OnLoaded = null)
        {
            AsyncOperationHandle<TObject> handle = default(AsyncOperationHandle<TObject>);

            //애셋 로드
            handle = Addressables.LoadAssetAsync<TObject>(address);



            var downloadStatus = handle.GetDownloadStatus();
            //Debug.Log(handle.PercentComplete); //returns an equally-weighted aggregate percentage of all the sub-operations that are complete.
            //Debug.Log(downloadStatus.TotalBytes);
            //Debug.Log(downloadStatus.DownloadedBytes);
            //Debug.Log(downloadStatus.Percent);//DownloadStatus.Percent reports the percentage of bytes downloaded.
            ////await handle.Task;
            //if (handle.Status == AsyncOperationStatus.Failed) 
            //{
            //    Debug.LogError($"{handle.OperationException.Message}");
            //}
            handle.Completed -= OnLoaded;
            handle.Completed += OnLoaded;

            return handle;
        }

        //public static TObject LoadAssetSync<TObject>(string assetPath)
        //{
        //    string strBundleName = GetAddress(bundleName, assetPath);
        //    return LoadAssetSync<TObject>(strBundleName);
        //}




        /// <summary>
        /// Address로 애셋 여러개 로드
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="addresses"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(List<string> addresses, Action<AsyncOperationHandle<IList<TObject>>> OnLoaded = null)
        {
            AsyncOperationHandle<IList<TObject>> handles = default(AsyncOperationHandle<IList<TObject>>);
            if (addresses.Count == 0)
            {
                return handles;
            }
            //애셋 로드
            handles = Addressables.LoadAssetsAsync<TObject>(addresses, obj => { }, Addressables.MergeMode.Union/*, releaseDependenciesOnFailure :true  - 여러 asset 중 하나라도 실패하면 모두 실패 처리*/  );

            handles.Completed -= OnLoaded;
            handles.Completed += OnLoaded;

            return handles;
        }



        public static TObject LoadAssetSync<TObject>(string address)
        {
            AsyncOperationHandle<TObject> handle = default(AsyncOperationHandle<TObject>);

            AsyncOperationHandle targetHandle;
            if (loadedHandles.TryGetValue(address, out targetHandle))
            {
                //저장된 핸들이 있다.
                Addressables.ResourceManager.Acquire(targetHandle);
                handle = targetHandle.Convert<TObject>();
                //handle.Result
            }
            else
            {
                handle = Addressables.LoadAssetAsync<TObject>(address);
                loadedHandles.Add(address, handle);

                handle.WaitForCompletion();

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError( $"Error:{address}");
                    Release(handle);
                }

            }

            if (handle.IsValid())
            {
                return handle.Result;
            }
            else
            {
                return default(TObject);
            }

            
        }



        private static void HandlePreloadingDownload(IEnumerable keys, Action<bool> OnDownload)
        {
            Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.Union, true).Completed += op =>
            {
                SpriteAtlasInit();
                OnDownload?.Invoke(op.Status == AsyncOperationStatus.Succeeded);
            };
        }

        private static void HandleGetDownloadSize(IEnumerable keys, Action<long> OnGetDownloadSize)
        {
            var getSizeHandle = Addressables.GetDownloadSizeAsync(keys);
            getSizeHandle.Completed += op =>
            {
                OnGetDownloadSize?.Invoke(op.Result);
                Addressables.Release(getSizeHandle);
            };
        }


        private static async UniTask HandleDownloadDependenciesAsync(IEnumerable keys, Action<bool> onCompleteDownload, Action<float> onProgressDownload)
        {
            var getDepenciesHandle = Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.Union);

            float progress = 0;

            while (getDepenciesHandle.Status == AsyncOperationStatus.None)
            {
                float percentageComplete = getDepenciesHandle.GetDownloadStatus().Percent;
                //float percentageComplete = downladStatus.Percent;
                if (percentageComplete > progress * 1.1) // Report at most every 10% or so
                {
                    progress = percentageComplete; // More accurate %
                    onProgressDownload?.Invoke(progress);
                }
                await UniTask.Yield();
            }

            onCompleteDownload?.Invoke(getDepenciesHandle.Status == AsyncOperationStatus.Succeeded);
            Addressables.Release(getDepenciesHandle);
        }


        private static (bool, AsyncOperationHandle) GetIsLoadedAsset(string address)
        {
            AsyncOperationHandle result = new AsyncOperationHandle();
            if (loadedHandles.TryGetValue(address, out result))
            {
                return (true, result);
            }

            return (false, result);
        }
        public static TObject LoadAssetSyncWithReference<TObject>(AssetReference assetReference)
        {

            AsyncOperationHandle op = assetReference.OperationHandle;
            AsyncOperationHandle<TObject> handle = default(AsyncOperationHandle<TObject>);

            if (assetReference.IsValid() && op.IsValid())
            {
                // Increase the usage counter & Convert.
                Addressables.ResourceManager.Acquire(op);
                handle = op.Convert<TObject>();

            }
            else
            {
                AsyncOperationHandle targetHandle;
                if (loadedHandles.TryGetValue(assetReference.RuntimeKey.ToString(), out targetHandle))
                {
                    //저장된 핸들이 있다.
                    //Addressables.ResourceManager.Acquire(targetHandle);
                    //handle = targetHandle.Convert<TObject>();
                    // Addressables.ResourceManager.Acquire(targetHandle);
                    handle = Addressables.LoadAssetAsync<TObject>(assetReference.RuntimeKey);
                    //handle.WaitForCompletion();
                }
                else
                {
                    handle = assetReference.LoadAssetAsync<TObject>();
                    handle.WaitForCompletion();
                }




            }


            if (!loadedHandles.ContainsKey(assetReference.RuntimeKey.ToString()))
            {
                loadedHandles.Add(assetReference.RuntimeKey.ToString(), op);
            }


            return handle.Result;
        }







        /// <summary>
        /// AssetReference로 애셋 로드
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="assetReference"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        public static AsyncOperationHandle<TObject> LoadAssetAsyncWithReference<TObject>(AssetReference assetReference, Action<AsyncOperationHandle<TObject>> OnLoaded = null)
        {
            AsyncOperationHandle op = assetReference.OperationHandle;
            AsyncOperationHandle<TObject> handle = default(AsyncOperationHandle<TObject>);

            if (assetReference.IsValid() && op.IsValid())
            {
                // Increase the usage counter & Convert.
                Addressables.ResourceManager.Acquire(op);
                handle = op.Convert<TObject>();

                if (handle.IsDone)
                {
                    handle.Completed -= OnLoaded;
                    OnLoaded?.Invoke(handle);
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

                handle = assetReference.LoadAssetAsync<TObject>();

                // Removed OnLoaded in-case it's already been added.
                handle.Completed -= OnLoaded;
                handle.Completed += OnLoaded;
            }

            return handle;
        }

        /// <summary>
        /// AssetReference로 게임오브젝트 생성
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        public static AsyncOperationHandle<GameObject> InstantiateAsyncWithReference(AssetReference assetReference, Action<AsyncOperationHandle<GameObject>> OnLoaded = null, Transform parent = null)
        {
            AsyncOperationHandle op = assetReference.OperationHandle;
            AsyncOperationHandle<GameObject> handle = default(AsyncOperationHandle<GameObject>);

            if (assetReference.IsValid() && op.IsValid())
            {
                // Increase the usage counter & Convert.
                Addressables.ResourceManager.Acquire(op);
                handle = op.Convert<GameObject>();

                if (handle.IsDone)
                {
                    OnLoaded?.Invoke(handle);
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

                handle = assetReference.InstantiateAsync(parent);

                // Removed OnLoaded in-case it's already been added.
                handle.Completed -= OnLoaded;
                handle.Completed += OnLoaded;
            }

            return handle;
        }


        public static GameObject InstantiateSyncWithReference(AssetReference assetReference)
        {
            AsyncOperationHandle op = assetReference.OperationHandle;
            AsyncOperationHandle<GameObject> handle = default(AsyncOperationHandle<GameObject>);



            if (assetReference.IsValid() && op.IsValid())
            {
                // Increase the usage counter & Convert.
                Addressables.ResourceManager.Acquire(op);
                handle = op.Convert<GameObject>();
            }
            else
            {

                handle = assetReference.InstantiateAsync();
                handle.WaitForCompletion();
            }

            return handle.Result;
        }


        /// <summary>
        /// 게임오브젝트 생성(비동기)
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetPath"></param>
        /// <param name="OnLoaded"></param>
        /// <returns></returns>
        //public static AsyncOperationHandle<GameObject> InstantiateAsync(string assetPath, Action<AsyncOperationHandle<GameObject>> OnLoaded = null, Transform parent = null)
        //{

        //    string strBundleName = GetAddress(bundleName, assetPath);

        //    return InstantiateAsync(strBundleName, OnLoaded, parent);
        //}


        public static AsyncOperationHandle<GameObject> InstantiateAsync(string address, Action<AsyncOperationHandle<GameObject>> OnLoaded = null, Transform parent = null)
        {
            //bundleName = bundleName.ToLower();
            AsyncOperationHandle<GameObject> handle = default(AsyncOperationHandle<GameObject>);
            handle = Addressables.InstantiateAsync(address, parent:parent);
            handle.Completed -= OnLoaded;
            handle.Completed += OnLoaded;

            return handle;
        }



        public static void ReleaseHandles()
        {
            //foreach (var item in _opHandles)
            //{

            //    Addressables.Release(item);
            //}

            //foreach (var item in loadedHandles)
            //{

            //    Addressables.Release(item);
            //}

        }
        /// <summary>
        /// Addressables.InstantiateAsync 생성된 게임 오브젝트 제거
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static bool ReleaseInstance(GameObject go)
        {
            return Addressables.ReleaseInstance(go);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public static void Release(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        public static void Clear()
        {
            //TODO : 클리어 처리 좀 더 명확하게
            //AssetBundle.UnloadAllAssetBundles(true);


            //Resources.UnloadUnusedAssets 
            // => Note that if you choose to use Resources.UnloadUnusedAssets, it is a very slow operation, and should only be called on a screen that won't show any hitches (such as a loading screen).
            //Note that the Unity runtime automatically calls UnloadUnusedAssets when you load a Scene using the LoadSceneMode.Single mode.



            //AssetReference 사용한 애셋 언로드(LoadAssetSyncWithReference) 
            //

            // Release asset when parent object is destroyed
            //private void OnDestroy()
            //{

            //    Addressables.Release(handle);
            //}
        }

















        public static string GetAddress(string assetPath)
        {
            //StringBuilder strBuilder = new StringBuilder();
            ////Assets
            //strBuilder.Append("Assets");
            //strBuilder.Append("/");
            //strBuilder.Append(bundleName);
            //strBuilder.Append("/");
            //strBuilder.Append(assetPath);
            //string strBundleName = strBuilder.ToString();

            //return strBundleName;
            return "";
        }

    }

}
