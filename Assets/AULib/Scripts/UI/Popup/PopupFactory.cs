using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AULib
{
    /// <summary>
    /// 扑诀 积己
    /// </summary>
    public static class PopupFactory
    {

        /// <summary>
        /// 扑诀 积己
        /// </summary>
        /// <param name="pop"></param>
        /// <returns></returns>
        public static PopupBase<T> Get<T>(string popPath, Transform parent) where T : IPopup
        {            
            PopupBase<T> popup;

            popup = GetPopupByPath<T>(popPath, parent);
            //popup.transform.SetParent(parent, false);
            return popup;

        }





        public static bool Destroy(GameObject go)
        {
            if (!Addressables.ReleaseInstance(go))
            {
                GameObject.Destroy(go);
            }
            return true;
        }




        private static PopupBase<T> GetPopupByPath<T>(string path, Transform parent) where T : IPopup
        {   
            GameObject obj = AddressableManager.LoadAssetSync<GameObject>("prefab", path);
            if (obj == null)
            {
                Debug.LogError($"Can not find object - path : {path}");
            }
            obj = GameObject.Instantiate(obj, parent, false);

            return obj.GetComponent<PopupBase<T>>();
        }
       


            
        
    }





}
