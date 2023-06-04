using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GameLogic.Photon
{
    public sealed class Level : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private float _delayToStartGame;
        
        private void Start()
        {
            _playerSpawner.SpawnPlayer();

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                StartCoroutine(WaitTimeToStartGame());
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            StartCoroutine(WaitTimeToStartGame());
        }

        private void StartGame()
        {
            print("StartGame");
        }

        private IEnumerator WaitTimeToStartGame()
        {
            yield return new WaitForSeconds(_delayToStartGame);
            
            StartGame();
        }
    }
}