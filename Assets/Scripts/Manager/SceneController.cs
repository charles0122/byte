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

        private IEnumerator LoadSceneAndSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newlyLoadedScene);
        }

        private IEnumerator Start()
        {
            faderImage.color = new Color(0f, 0f, 0f, 1f);
            // faderCanvasGroup.alpha = 1;
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


        



    }
}