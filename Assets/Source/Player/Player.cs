using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PhotonView))]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private Sprite _newSprite;
        
        private SpriteRenderer _spriteRenderer;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (_photonView.IsMine == false)
                _spriteRenderer.sprite = _newSprite;
        }
    }
}