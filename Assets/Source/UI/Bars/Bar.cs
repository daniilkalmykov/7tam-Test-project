using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public abstract class Bar : MonoBehaviour
    {
        private const float MaxFillAmount = 1;

        [SerializeField] private float _speed;
        [SerializeField] private Image _fillImage;

        private Coroutine _coroutine;

        private void Start()
        {
            _fillImage.fillAmount = MaxFillAmount;
        }
        
        protected void StartChangeFillAmount(float value)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeFillAmount(value));
        }
        
        private IEnumerator ChangeFillAmount(float value)
        {
            while (_fillImage.fillAmount != value)
            {
                _fillImage.fillAmount = Mathf.MoveTowards(_fillImage.fillAmount, value, _speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}