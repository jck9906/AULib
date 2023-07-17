using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// 캔버스 정보 셋팅용 데이터 클래스
    /// </summary>
    [CreateAssetMenu(fileName = "NewCanvasSetData", menuName = "AULib/Create Canvas set data", order = 0)]
    public class CanvasSetData : ScriptableObject
    {
        [Multiline]
        public string Guide;
             
        //
        public int SortOrder;

        //
        public bool PixelPerfect;


        public RenderMode RenderMode;

        public int SortingLayerID;

        
    }
}
