using System;
using Photon.Pun;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic.Photon
{
    public sealed class PlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Player.Player _prefab;
        [SerializeField] private int _minXPosition;
        [SerializeField] private int _maxXPosition;
        [SerializeField] private int _minYPosition;
        [SerializeField] private int _maxYPosition;
        [SerializeField] private Joystick _joystick;

        public void SpawnPlayer()
        {
            var playerXPosition = Random.Range(_minXPosition, _maxXPosition);
            var playerYPosition = Random.Range(_minYPosition, _maxYPosition);

            var playerPosition = new Vector2(playerXPosition, playerYPosition);

            var player = PhotonNetwork.Instantiate(_prefab.name, playerPosition, Quaternion.identity);

            InitPlayer(player);
        }

        private void InitPlayer(GameObject player)
        {
            if (player.TryGetComponent(out PlayerInput playerInput) == false)
                throw new ArgumentNullException();

            playerInput.Init(_joystick);
        }
    }
}