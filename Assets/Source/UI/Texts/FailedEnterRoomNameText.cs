using System.Collections.Generic;
using UI.Buttons;
using UnityEngine;

namespace UI.Texts
{
    public sealed class FailedEnterRoomNameText : GameText
    {
        [SerializeField] private List<LobbyButton> _lobbyButtons = new();

        private void OnEnable()
        {
            foreach (var lobbyButton in _lobbyButtons)
                lobbyButton.ConnectionFailed += OnConnectionFailed;
        }

        private void OnDisable()
        {
            foreach (var lobbyButton in _lobbyButtons)
                lobbyButton.ConnectionFailed -= OnConnectionFailed;
        }

        private void OnConnectionFailed()
        {
            Output();
        }
    }
}