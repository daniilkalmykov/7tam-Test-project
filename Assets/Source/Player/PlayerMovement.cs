using Constants;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(CharacterController), typeof(SpriteRenderer),typeof(Animator))]
    public sealed class PlayerMovement : MonoBehaviour, IPunObservable
    {
        [SerializeField] private float _speed;
        
        private CharacterController _characterController;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private PhotonView _photonView;
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _characterController = GetComponent<CharacterController>();
            
            AddObservable();
        }

        public void Move(float xDirection, float yDirection)
        {
            var direction = new Vector2(xDirection, yDirection) * (_speed * Time.deltaTime);
            _characterController.Move(direction);

            _spriteRenderer.flipX = xDirection switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };

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