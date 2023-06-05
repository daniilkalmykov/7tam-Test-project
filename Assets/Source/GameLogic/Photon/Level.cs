using System.Collections;
using Photon.Pun;
using UI.Buttons;
using UnityEngine;

namespace GameLogic.Photon
{
    public sealed class Level : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private float _delayToStartGame;
        [SerializeField] private CoinsSpawner _coinsSpawner;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private ShootingButton _shootingButton;
        
        private void Start()
        {
            _playerSpawner.SpawnPlayer();

            _joystick.enabled = false;
            _shootingButton.enabled = false;

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                StartCoroutine(WaitTimeToStartGame());
        }

        public override void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                StartCoroutine(WaitTimeToStartGame());
        }

        private void StartGame()
        {
            _coinsSpawner.Spawn();
            
            _joystick.enabled = true;
            _shootingButton.enabled = true;
        }

        private IEnumerator WaitTimeToStartGame()
        {
            yield return new WaitForSeconds(_delayToStartGame);
            
            StartGame();
        }
    }
}