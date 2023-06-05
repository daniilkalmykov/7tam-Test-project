using System;
using Photon.Pun;
using Player;
using UI;
using UI.Bars;
using UI.Buttons;
using UnityEngine;

namespace GameLogic
{
    public sealed class ObjectsInitializer : MonoBehaviour
    {
        [SerializeField] private ShootingButton _shootingButton;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private EndScreen _endScreen;

        public void InitObjects(GameObject player)
        {
            if (player.TryGetComponent(out PlayerShooting playerShooting) == false)
                throw new ArgumentNullException();

            if (player.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new ArgumentNullException();

            _shootingButton.Init(playerShooting);
            _healthBar.Init(playerHealth);
            _endScreen.Init(playerHealth);
        }
    }
}