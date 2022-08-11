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
    /// 폰트 관리
    /// </summary>
    public class FontManager<T> : MonoSingletonBase<FontManager<T>>
    {

       

        //uGUI 폰트
        [SerializeField] private T _fontNormal;
        [SerializeField] private T _fontBold;

        //TMP 폰트
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
                Debug.LogWarning("셋팅 된 기본 폰트가 없습니다. 기본 폰트를 셋팅해 주세요.");
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
        Normal, //일반 텍스트
        Bold    //강조용 텍스트
    }
}
