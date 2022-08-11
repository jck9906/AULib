using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ToggleItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Background;
    public Text textName;


    public void SetToggleItem(string name, string bundle, string path)
    {
        textName.text = name;
        //Background.sprite = AssetManager.i.LoadAsset<Sprite>(bundle, path);
      
        //AddressableManager.LoadAssetAsync<Sprite>(bundle, path, (op) =>
        //{
        //    Background.sprite = op.Result;
        //});
        
    }
}
