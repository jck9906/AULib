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
    /// ���� ���� �� �׻� �ʿ��� ������Ʈ��
    /// </summary>
    public class GameController : MonoSingleton<GameController>
    {

        [SerializeField] private bool _isLockFrameRate;
        [SerializeField] private int _targetFrameRate;

        /// <summary>
        /// �ý��� ���û��� Fixed TimeStep
        /// </summary>
        [ReadOnly][SerializeField] private float _systemFixedDeltaTime;

        public float SystemFixedDeltaTime => _systemFixedDeltaTime;


#if UNITY_EDITOR
        public static void InstantiateGameController()
        {
            AULibSetting.Init();
            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AULibSetting.GAME_CONTROLLER_PATH));
            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AULibSetting.UI_MANAGER_PATH));            
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
			else
			{
				Application.targetFrameRate = -1;
			}

            _systemFixedDeltaTime = Time.fixedDeltaTime;
        }


        /// <summary>
        /// Set Fixed Time Step 
        /// ��� �� ���� �ʿ�!!
        /// </summary>
        /// <param name="timeStep"></param>
        public void SetFixedTimeStep(float timeStep)
        {
            Time.fixedDeltaTime = timeStep;
        }

        /// <summary>
        /// Fixed Time Step �ý��� �������� ����
        /// </summary>
        public void RestoreFixedTimeStep()
        {
            Time.fixedDeltaTime = _systemFixedDeltaTime;
        }

    }
}