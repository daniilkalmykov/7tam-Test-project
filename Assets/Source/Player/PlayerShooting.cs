using System;
using Infrastructure;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private Transform _flippedShootingPoint;
        [SerializeField] private float _shootingDistance;
        
        private SpriteRenderer _spriteRenderer;
        private PhotonView _photonView;
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [PunRPC]
        public void Shoot()
        {
            if (_photonView.IsMine == false)
                return;
            
            var position = _spriteRenderer.flipX == false ? _shootingPoint.position : _flippedShootingPoint.position;

            var newBullet = PhotonNetwork.Instantiate(_bullet.name, position, Quaternion.identity);

            if (newBullet.TryGetComponent(out Bullet bullet) == false)
                throw new ArgumentNullException();

            if (newBullet.TryGetComponent(out SpriteRenderer spriteRenderer) == false)
                throw new ArgumentNullException();

            var positionX = _spriteRenderer.flipX == false ? position.x + _shootingDistance : position.x - _shootingDistance;
            bullet.Init(new Vector2(positionX, position.y));

            spriteRenderer.flipX = _spriteRenderer.flipX;
        }
    }
}