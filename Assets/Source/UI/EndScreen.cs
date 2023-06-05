using System;
using Photon.Pun;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(PhotonView), typeof(CanvasGroup))]
    public sealed class EndScreen : MonoBehaviour, IPunObservable
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private RectTransform _playerNickNameSpawnPoint;
        [SerializeField] private RectTransform _coinsTextSpawnPoint;

        private PlayerHealth _player;
        private PhotonView _photonView;
        private CanvasGroup _canvasGroup;
        private PlayerCoinsCollector _playerCoinsCollector;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _photonView = GetComponent<PhotonView>();
            
            AddObservable();
        }

        private void OnDisable()
        {
            _player.Died -= OnDied;
        }

        private void Start()
        {
            ChangeVisibility(false);
        }

        public void Init(PlayerHealth player, PlayerCoinsCollector playerCoinsCollector)
        {
            _player = player;
            _playerCoinsCollector = playerCoinsCollector;

            _player.Died += OnDied;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_canvasGroup.alpha);
                stream.SendNext(_canvasGroup.interactable);
                stream.SendNext(_canvasGroup.blocksRaycasts);
            }
            else
            {
                _canvasGroup.alpha = (float)stream.ReceiveNext();
                _canvasGroup.interactable = (bool)stream.ReceiveNext();
                _canvasGroup.blocksRaycasts = (bool)stream.ReceiveNext();
            }
        }
        
        private void OnDied()
        {
            ChangeVisibility(true);

            SetPlayerNickName();
            SetCoinsText();
        }

        private void SetPlayerNickName()
        {
            SetText(_playerName, _playerNickNameSpawnPoint, PhotonNetwork.NickName);
        }

        private void SetCoinsText()
        {
            SetText(_coinsText, _coinsTextSpawnPoint, _playerCoinsCollector.CoinsCount.ToString());
        }

        private void SetText(TMP_Text tmpText, Transform spawnPoint, string text)
        {
            var newText = PhotonNetwork.Instantiate(tmpText.name, Vector3.zero, Quaternion.identity)
                .GetComponent<TMP_Text>();
            
            if (newText.TryGetComponent(out PhotonView photonView) == false)
                throw new ArgumentNullException();

            var photonViewTransform = photonView.transform;
            
            photonViewTransform.SetParent(transform);
            photonViewTransform.position = spawnPoint.position;
            
            newText.text = text;
        }

        private void ChangeVisibility(bool state)
        {
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;

            _canvasGroup.alpha = state ? 1 : 0;
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}