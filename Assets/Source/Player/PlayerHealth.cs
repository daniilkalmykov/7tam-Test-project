using System;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class PlayerHealth : MonoBehaviour, IPunObservable
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            
            AddObservable();
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TryTakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentNullException();

            _currentHealth -= damage;

            if (_currentHealth <= 0)
                _photonView.gameObject.SetActive(false);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(_currentHealth);
            else
                _currentHealth = (int)stream.ReceiveNext();
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}