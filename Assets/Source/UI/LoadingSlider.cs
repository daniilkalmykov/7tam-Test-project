using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public sealed class LoadingSlider : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _sceneLoader.ProgressChanged += OnProgressChanged;
        }

        private void OnDisable()
        {
            _sceneLoader.ProgressChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _slider.value = progress;
        }
    }
}