using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AULib;

namespace AULib
{

    /// <summary>
    /// ĵ���� ���� ���ÿ� ������ Ŭ����
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
