using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneName _sceneName;
        
        public event Action<float> ProgressChanged;
        
        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneName.ToString());

            while (asyncOperation.isDone == false)
            {
                ProgressChanged?.Invoke(Mathf.Clamp01(asyncOperation.progress));

                yield return null;
            }
        }
    }
}
