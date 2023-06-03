using System;
using Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI.Buttons
{
    public abstract class LobbyButton : GameButton
    {
        [SerializeField] private TMP_InputField _roomName;

        public event Action ConnectionFailed;
            
        [field: SerializeField] protected LobbyConnector LobbyConnector { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            LobbyConnector.PlayerConnected += OnPlayerConnected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            LobbyConnector.PlayerConnected -= OnPlayerConnected;
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnected)
                return;
            
            Button.interactable = false;
        }

        protected bool CanBePressed()
        {
            var canBePressed = string.IsNullOrEmpty(_roomName.text) == false;

            if (canBePressed) 
                return true;
            
            ConnectionFailed?.Invoke();

            return false;
        }

        protected string GetRoomName()
        {
            return _roomName.text;
        }
        
        private void OnPlayerConnected()
        {
            Button.interactable = true;
        }
    }
}