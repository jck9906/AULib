using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AULib
{

    /// <summary>
    /// 아틀라스 파일 목록 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "NewSpriteAtlasData", menuName = "AULib/Create Sprite Atlas Data", order = 0)]
    public class SpriteAtlasData : ScriptableObject
    {
        [SerializeField] private List<SpriteAtlasInfo> SpriteAtlases = new();



        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="atlasName"></param>
        public void AddSpriteAtlas(string atlasName, bool isBundle)
        {
            SpriteAtlases.Add(new SpriteAtlasInfo() { AtlasName = atlasName, IsBundle = isBundle });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atlasName"></param>
        /// <returns></returns>
        public bool IsContain(string atlasName)
        {
            return SpriteAtlases.Any( item => item.AtlasName.Equals(atlasName));
        }


        public void Clear()
        {
            SpriteAtlases.Clear();
        }
    }


    [Serializable]
    public class SpriteAtlasInfo
    {
        public string AtlasName;
        public bool IsBundle;
    }
}
