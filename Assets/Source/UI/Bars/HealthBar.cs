using Player;
using UnityEngine;

namespace UI.Bars
{
    public sealed class HealthBar : Bar
    {
        [SerializeField] private PlayerHealth _playerHealth;

        private void OnDisable()
        {
            _playerHealth.Changed -= OnChanged;
        }

        public void Init(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            
            _playerHealth.Changed += OnChanged;
            _playerHealth.Died += OnDied;
        }

        private void OnDied()
        {
            StartChangeFillAmount(0);
        }

        private void OnChanged(int currentHealth, int maxHealth)
        {
            StartChangeFillAmount((float)currentHealth / maxHealth);
        }
    }
}