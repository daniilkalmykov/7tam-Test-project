using System;
using Enums;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Photon
{
    public sealed class LobbyConnector : MonoBehaviourPunCallbacks
    {
        private const string GameVersion = "0.0.0.1";

        [SerializeField] private int _minPlayerIndex;
        [SerializeField] private int _maxPlayerIndex;

        public event Action PlayerConnected;
        public event Action PlayerConnectFailed;
        
        [field: SerializeField] public int MaxPlayers { get; private set; }

        private void Start()
        {
            PhotonNetwork.NickName = "Player" + Random.Range(_minPlayerIndex, _maxPlayerIndex);
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.AutomaticallySyncScene = true;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PlayerConnected?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(SceneName.Level.ToString());
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            PlayerConnectFailed?.Invoke();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            PlayerConnectFailed?.Invoke();
        }
    }
}