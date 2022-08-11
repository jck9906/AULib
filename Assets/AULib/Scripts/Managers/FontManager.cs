using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AULib
{
    /// <summary>
    /// ��Ʈ ����
    /// </summary>
    public class FontManager<T> : MonoSingletonBase<FontManager<T>>
    {

       

        //uGUI ��Ʈ
        [SerializeField] private T _fontNormal;
        [SerializeField] private T _fontBold;

        //TMP ��Ʈ
        //[SerializeField] private TMP_FontAsset _fontNormalTMP;
        //[SerializeField] private TMP_FontAsset _fontBoldTMP;


#if UNITY_EDITOR
        private const string PREFAB_PATH = "Assets/AULib/Prefabs/FontManager.prefab"; 
        public static FontManager<T> Get()
        {
            return AssetDatabase.LoadAssetAtPath<FontManager<T>>(PREFAB_PATH);
        }
#endif

        public override void Init()
        {
            if (_fontNormal == null || _fontBold == null)
            {
                Debug.LogWarning("���� �� �⺻ ��Ʈ�� �����ϴ�. �⺻ ��Ʈ�� ������ �ּ���.");
            }
        }



        public T GetFont(eFontType fontType) => fontType switch
        {
            eFontType.Normal => _fontNormal,
            eFontType.Bold => _fontBold,
        };
            
        
    }

    public enum eFontType
    {
        Normal, //�Ϲ� �ؽ�Ʈ
        Bold    //������ �ؽ�Ʈ
    }
}
