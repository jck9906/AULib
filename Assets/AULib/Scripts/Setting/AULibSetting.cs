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
                    Debug.LogError("�ε��� �̸��� ���� �ȵǾ����ϴ�. ���ÿ��� Loading Scene Name �ν����͸� Ȯ���� �ּ���.");
                }
                return s_instance.LoadingSceneName;
            }

        }


        [Header("Atlas")]
        [Tooltip("��Ʋ�� �ּ� ���")]
        public string AtalasPath;
        public static string ATLAS_PATH => s_instance.AtalasPath;

        //��Ʋ�� ���
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

        [Tooltip("�����Ϳ��� ��Ʋ�� ���� ��� ����")]
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
