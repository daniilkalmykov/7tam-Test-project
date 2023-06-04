using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace GameLogic.Photon
{
    public sealed class Level : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private float _delayToStartGame;
        [SerializeField] private CoinsSpawner _coinsSpawner;
        
        private void Start()
        {
            _playerSpawner.SpawnPlayer();

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
            print("start");
            _coinsSpawner.Spawn();
        }

        private IEnumerator WaitTimeToStartGame()
        {
            yield return new WaitForSeconds(_delayToStartGame);
            
            StartGame();
        }
    }
}