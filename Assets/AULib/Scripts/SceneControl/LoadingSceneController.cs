using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using Cysharp.Threading.Tasks;

// public class LoadingSceneManager : MonoBehaviour

namespace AULib
{
    public class LoadingSceneController : MonoSingletonBase<LoadingSceneController>, IBackable
    {
        public static string nextScene;
        public static string currentScene => SceneManager.GetActiveScene().name;

        [SerializeField] string loadingSceneName;
        [SerializeField] Slider progressBar;
        [SerializeField] TextMeshProUGUI textTip;
        [SerializeField] AudioListener audioListener;


        //private static LoadingSceneManager thisInstance;
        //private void Awake()
        //{
        //    thisInstance = this;
        //}

        protected override void Start()
        {
            LoadingSceneAsync().Forget();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        public static void LoadScene(string scene)
        {
            //ALPlayerData.i.prevSceneID = SceneManager.GetActiveScene().buildIndex;
            nextScene = scene;
            SceneManager.LoadScene(i.loadingSceneName);
        }

        public static void SceneLoadCallback(BaseSceneController sceneController)
        {
            if (!IsValid())
            {
                //씬에서 바로 실행 시 처리
                sceneController.InitializeFinished();
            }
            else
            {
                s_instance.InitializingSceneAsync(sceneController).Forget();
            }
        }



        async UniTaskVoid LoadingSceneAsync()
        {

            UpdateTip("Scene is loading...");
            await UniTask.Yield();

            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            op.allowSceneActivation = false;

            float timer = 0.0f;

            while (!op.isDone)
            {
                await UniTask.Yield();
                timer += Time.deltaTime;

                if (op.progress < 0.9f)
                {
                    UpdateProgressBar(Mathf.Lerp(progressBar.value, op.progress, timer));

                    if (progressBar.value >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    UpdateProgressBar(Mathf.Lerp(progressBar.value, 1f, timer));
                    if (progressBar.value == 1.0f)
                    {
                        op.allowSceneActivation = true;
                    }
                }
            }
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            if (mode == LoadSceneMode.Additive)
            {
                SceneManager.SetActiveScene(scene);
                audioListener.enabled = false;
            }
        }

        private void UpdateProgressBar(float val)
        {
            progressBar.value = val;
        }

        private void UpdateTip(string tip)
        {
            textTip.text = tip;
        }


        private async UniTaskVoid InitializingSceneAsync(BaseSceneController sceneController)
        {
            sceneController.InitializeSceneAsync().Forget();
            while (!sceneController.IsDone)
            {
                UpdateTip(sceneController.LoadingTipMessage);
                UpdateProgressBar(sceneController.Progress);
                await UniTask.NextFrame();
            }

            await SceneManager.UnloadSceneAsync(i.loadingSceneName);
            sceneController.InitializeFinished();
        }






        #region Implements IBackable
        public void OnBackButtonInput()
        {
            Debug.Log("로딩중에는 Back button 사용불가!");
        }
        #endregion Implements IBackable
    }
}