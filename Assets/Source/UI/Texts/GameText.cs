using TMPro;
using UnityEngine;

namespace UI.Texts
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class GameText : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private string _startText;
        
        private TMP_Text _tmpText;
        
        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _tmpText.text = _startText;
        }

        protected void Output()
        {
            _tmpText.text = _text;
        }
    }
}