using Constants;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PhotonView), typeof(SpriteRenderer),typeof(Animator))]
    public sealed class PlayerMovement : MonoBehaviour, IPunObservable
    {
        [SerializeField] private float _speed;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private PhotonView _photonView;
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            AddObservable();
        }

        public void Move(float xDirection, float yDirection)
        {
            var direction = new Vector2(xDirection, yDirection) * _speed;
            
            _spriteRenderer.flipX = xDirection switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };

            _rigidbody.velocity = direction;
            
            _animator.SetBool(AnimatorParameters.IsRunning, direction.magnitude > 0);
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