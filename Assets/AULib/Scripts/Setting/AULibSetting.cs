using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    [CreateAssetMenu(fileName = "AULibSetting", menuName = "AULib/CreateSetting", order = 1)]
    public class AULibSetting : ScriptableObject
    {

        [Header("Asset")]
        
        public string GameControllerPath;
        public static string GAME_CONTROLLER_PATH => s_instance.GameControllerPath;

        public string CommonUIControllerPath;
        public static string COMMON_UI_CONTROLLER_PATH => s_instance.CommonUIControllerPath;


        [Header("Atlas")]
        [Tooltip("아틀라스 애셋 경로")]
        public string AtalasPath;        
        public static string ATLAS_PATH => s_instance.AtalasPath;

        [Tooltip("에디터에서 아틀라스 파일 사용 여부")]
        public bool UseAtlasOnEditor;
        public static bool USE_ATLAS_ON_EDITOR => s_instance.UseAtlasOnEditor;

        protected static AULibSetting s_instance = null;

        [Header("Network")]
        [Tooltip("Web URL")]
        public string WebURL;
        public static string WEB_URL => s_instance.WebURL;

        public static void Init()
        {
            if (s_instance == null)
            {
                s_instance = Resources.Load<AULibSetting>("AULibSetting");
            }            
        }



        
    }
}
