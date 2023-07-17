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

        public string UIManagerPath;
        public static string UI_MANAGER_PATH => s_instance.UIManagerPath;
    
        public string LoadingSceneName;
        public static string LOADING_SCENE_NAME
        {
            get
            {
                if (string.IsNullOrEmpty(s_instance.LoadingSceneName))
                {
                    Debug.LogError("로딩씬 이름이 설정 안되었습니다. 셋팅에서 Loading Scene Name 인스펙터를 확인해 주세요.");
                }
                return s_instance.LoadingSceneName;
            }

        }


        [Header("Atlas")]
        [Tooltip("아틀라스 애셋 경로")]
        public string AtalasPath;
        public static string ATLAS_PATH => s_instance.AtalasPath;

        //아틀라스 목록
        public string AtlasListDataPath;
        public static string ATLAS_LIST_DATA_PATH => s_instance.AtlasListDataPath;

        public string RawSpriteBuildPath;
        public static string RAW_SPRITE_BUILD_PATH => s_instance.RawSpriteBuildPath;

        public string RawSpriteBundlePath;
        public static string RAW_SPRITE_BUNDLE_PATH => s_instance.RawSpriteBundlePath;


        public string AtlasCreateBuildPath;
        public static string ATLAS_CREATE_BUILD_PATH => s_instance.AtlasCreateBuildPath;

        public string AtlasCreateBundlePath;
        public static string ATLAS_CREATE_BUNDLE_PATH => s_instance.AtlasCreateBundlePath;

        public int AtlasMaxSize;
        public static int ATLAS_MAX_SIZE => s_instance.AtlasMaxSize;

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
