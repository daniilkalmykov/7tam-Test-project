using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView), typeof(PlayerMovement))]
    public sealed class PlayerInput : MonoBehaviour
    {
        private Joystick _joystick;
        private PhotonView _photonView;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (_photonView.IsMine == false)
                return;

            _playerMovement.Move(_joystick.Horizontal, _joystick.Vertical);
        }

        public void Init(Joystick joystick)
        {
            _joystick = joystick;
        }
    }
}