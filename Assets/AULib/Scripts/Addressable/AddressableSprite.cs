using Cysharp.Threading.Tasks;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AULib
{

    [CreateAssetMenu(fileName = "NewAddressableSprite", menuName = "AULib/Create Addressable sprite", order = 0)]
    public class AddressableSprite : ScriptableObject
    {
        [Multiline]
        public string Guide;


        public AssetReferenceSprite spriteReference;

        //�ּ°�� ���� �Ѵ�
        [ReadOnly] public string spriteAddress;



        private Sprite _sprite;
        private void OnValidate()
        {
#if UNITY_EDITOR
            if (spriteReference != null)
            spriteAddress = AssetDatabase.GetAssetPath(spriteReference.editorAsset);
#endif
        }








        public void GetSprite(Action<Sprite> onGet)
        {
            if(_sprite == null)
            {
                AddressableManager.LoadSpriteAsync(spriteAddress, sprite =>
                {
                    _sprite = sprite;
                    onGet?.Invoke(_sprite);
                });
            }
            else
            {
                onGet?.Invoke(_sprite);
            }
        }
    }
}
