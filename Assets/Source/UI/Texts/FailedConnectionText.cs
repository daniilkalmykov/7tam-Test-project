using GameLogic.Photon;
using Photon;
using TMPro;
using UnityEngine;

namespace UI.Texts
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class FailedConnectionText : GameText
    {
        [SerializeField] private LobbyConnector _lobbyConnector;

        private void OnEnable()
        {
            _lobbyConnector.PlayerConnectFailed += OnPlayerConnectFailed;
        }

        private void OnDisable()
        {
            _lobbyConnector.PlayerConnectFailed -= OnPlayerConnectFailed;
        }
        
        private void OnPlayerConnectFailed()
        {
            Output();
        }
    }
}