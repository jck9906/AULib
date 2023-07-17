using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AULib.Editor
{
    public class AULibMenuItems 
    {
        //셋팅 파일
        private const string SETTING_FILE_PATH = "Assets/AULib/Resources/AULibSetting.asset";
        //사운드 매니저 프리팹
        private const string SOUND_MANAGER_PREFAB_PATH = "Assets/AULib/Prefabs/SoundManager.prefab";
        //폰트 매니저 프리팹
        private const string FONT_MANAGER_PREFAB_PATH = "Assets/AULib/Prefabs/FontManager.prefab";

        [MenuItem("AULib/Setting", false, 1)]
        public static void OpenSetting()
        {

            if (!File.Exists(SETTING_FILE_PATH))
            {
                Debug.Log("셋팅 파일이 없으니 새로 만듭니다.");
                AULibSetting setting = ScriptableObject.CreateInstance<AULibSetting>();
                AssetDatabase.CreateAsset(setting, SETTING_FILE_PATH);
                AssetDatabase.Refresh();
            }

            Selection.activeObject = AssetDatabase.LoadAssetAtPath<AULibSetting>(SETTING_FILE_PATH);
        }



        [MenuItem("AULib/Open SoundManager", false, 2)]
        public static void OpenSoundManager()
        {
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<SoundManager>(SOUND_MANAGER_PREFAB_PATH));
        }


        [MenuItem("AULib/Open FontManager", false, 3)]
        public static void OpenFontManager()
        {
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<GameObject>(FONT_MANAGER_PREFAB_PATH));
        }


        [MenuItem("AULib/Build sprite atlas", false, 4)]
        public static void BuildSpriteAtlas()
        {
            SpriteAtlasBuilder.BuildSpriteAtlas();
        }

    }
}
