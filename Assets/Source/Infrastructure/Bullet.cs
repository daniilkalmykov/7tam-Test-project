using Photon.Pun;
using Player;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        private Vector3 _target;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        private void Update()
        {
            if (_photonView.IsMine == false)
                return;
            
            var speed = _speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, _target, speed);

            if (_target == transform.position)
                PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TryTakeDamage(_damage);
            }
            
            PhotonNetwork.Destroy(gameObject);
        }

        public void Init(Vector2 target)
        {
            _target = target;
        }
    }
}