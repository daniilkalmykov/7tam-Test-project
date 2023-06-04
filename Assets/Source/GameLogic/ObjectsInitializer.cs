using System;
using Player;
using UI.Buttons;
using UnityEngine;

namespace GameLogic
{
    public sealed class ObjectsInitializer : MonoBehaviour
    {
        [SerializeField] private ShootingButton _shootingButton;

        public void InitObjects(GameObject player)
        {
            if (player.TryGetComponent(out PlayerShooting playerShooting) == false)
                throw new ArgumentNullException();
            
            _shootingButton.Init(playerShooting);
        }
    }
}