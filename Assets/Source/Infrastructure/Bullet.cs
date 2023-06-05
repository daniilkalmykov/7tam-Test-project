using Photon.Pun;
using Player;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(PhotonView), typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour, IPunObservable
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        private Vector3 _target;
        private PhotonView _photonView;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _photonView = GetComponent<PhotonView>();
            
            AddObservable();
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(_spriteRenderer.flipX);
            else
                _spriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}