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

        private PlayerHealth _player;
        private PhotonView _photonView;
        private CanvasGroup _canvasGroup;

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

        public void Init(PlayerHealth player)
        {
            _player = player;

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

            var nickName = PhotonNetwork.Instantiate(_playerName.name, Vector3.zero, Quaternion.identity).GetComponent<TMP_Text>();

            if (nickName.TryGetComponent(out PhotonView photonView) == false)
                throw new ArgumentNullException();

            photonView.transform.SetParent(transform);
            nickName.text = PhotonNetwork.NickName;
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