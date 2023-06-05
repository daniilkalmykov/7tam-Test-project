using Infrastructure;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class PlayerWallet : MonoBehaviour, IPunObservable
    {
        private PhotonView _photonView;
        private int _coinsCount;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            
            AddObservable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin) == false) 
                return;
            
            _coinsCount++;
            
            PhotonNetwork.Destroy(coin.gameObject);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(_coinsCount);
            else
                _coinsCount = (int)stream.ReceiveNext();
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}