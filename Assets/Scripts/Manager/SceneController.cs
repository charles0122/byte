using System.Collections;
using UnityEngine;
using ByteLoop.Tool;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Mathematics;

namespace ByteLoop.Manager
{

    public class SceneController : PersistentMonoSingleton<SceneController>
    {
        private bool isFading;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private CanvasGroup faderCanvasGroup = null;
        [SerializeField] private Image faderImage = null;
        public string StartingSceneName;

        public void FadeAndLoadScene(string sceneName )
        {
            if (!isFading)
            {
                StartCoroutine(FadeAndSwitchScenes(sceneName));
            }
        }
        private IEnumerator FadeAndSwitchScenes(string sceneName )
        {
            EventCenter.CallBeforeSceneUnloadFadeOutEvent();
            yield return StartCoroutine(Fade(2f));

            EventCenter.CallBeforeSceneUnloadEvent();

            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

            EventCenter.CallAfterSceneloadEvent();
            yield return StartCoroutine(Fade(0f));
            EventCenter.CallAfterSceneloadFadeInEvent();

        }
        private IEnumerator LoadSceneAndSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newlyLoadedScene);
        }

        private IEnumerator Start()
        {
            faderImage.color = new Color(0f, 0f, 0f, 1f);
            faderCanvasGroup.alpha = 1;
            yield return StartCoroutine(LoadSceneAndSetActive(StartingSceneName.ToString()));
            EventCenter.CallAfterSceneloadEvent();
     
            StartCoroutine(Fade(0f));

        }
        private IEnumerator Fade(float finalAlpha)
        {
            isFading = true;
            faderCanvasGroup.blocksRaycasts = true;

            float fadeSpeed = math.abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
            while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
            {
                faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
                yield return null;
            }
            isFading = false;
            faderCanvasGroup.blocksRaycasts = false;

        }

        /// <summary>
        /// 切换场景 同步加载
        /// </summary>
        /// <param name="name">场景名字</param>
        public void LoadScene(string name, LoadSceneMode mode)
        {
            if (IsValidCanLoadScene(name))
            {
                SceneManager.LoadScene(name, mode);

            }
        }
        /// <summary>
        /// 切换场景 同步加载
        /// </summary>
        /// <param name="name">场景名字</param>
        public void LoadScene(string name, UnityAction fuction)
        {
            if (IsValidCanLoadScene(name))
            {
                SceneManager.LoadScene(name);

                fuction();
            }
        }

        /// <summary>
        /// 切换场景 异步加载
        /// </summary>
        /// <param name="name">场景名字</param>
        /// <param name="fuction"></param>
        public void LoadSceneAsync(string name, UnityAction<AsyncOperation> fuction)
        {
            if (IsValidCanLoadScene(name))
            {
                StartCoroutine(ReallyLoadSceneAsync(name, fuction));
            }
        }

        IEnumerator ReallyLoadSceneAsync(string name, UnityAction<AsyncOperation> fuction)
        {
            yield return null;

            AsyncOperation AO = SceneManager.LoadSceneAsync(name);

            AO.allowSceneActivation = false;
            fuction(AO);

            while (!AO.isDone)
            {
                //事件中心向外分发场景进度
                EventCenter.Instance.EventTrigger("Loading");

                yield return AO.progress;
            }

            yield return AO;
        }

        /// <summary>
        /// 强制GC
        /// </summary>
        public void ToGC()
        {
            System.GC.Collect();
        }


        /// <summary>
        /// 验证场景是否存在
        /// </summary>
        /// <param name="scenename"></param>
        /// <returns></returns>
        public bool IsValidCanLoadScene(string scenename)
        {
            if (Application.CanStreamedLevelBeLoaded(scenename))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}