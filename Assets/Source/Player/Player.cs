using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PhotonView), typeof(Animator))]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private Sprite _enemySprite;
        [SerializeField] private RuntimeAnimatorController _enemyAnimatorController;
        
        private Animator _animator;
        private PhotonView _photonView;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _photonView = GetComponent<PhotonView>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (_photonView.IsMine)
                return;
            
            _spriteRenderer.sprite = _enemySprite;
            _animator.runtimeAnimatorController = _enemyAnimatorController;
        }
    }
}