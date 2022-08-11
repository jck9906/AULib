using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AULib
{
    /// <summary>
    /// GameController
    /// 게임 실행 시 항상 필요한 오브젝트들
    /// </summary>
    public class GameController : MonoSingleton<GameController>
    {

        [SerializeField] private bool _isLockFrameRate;
        [SerializeField] private int _targetFrameRate;

#if UNITY_EDITOR
        public static void InstantiateGameController()
        {
            AULibSetting.Init();
            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AULibSetting.GAME_CONTROLLER_PATH));
            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AULibSetting.COMMON_UI_CONTROLLER_PATH));
        }

#endif

        public override void Init()
        {

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            if (_isLockFrameRate)
            {
                Application.targetFrameRate = _targetFrameRate;
                QualitySettings.vSyncCount = 0;
            }
            AULibSetting.Init();
        }

    }
}