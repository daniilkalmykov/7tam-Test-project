using Infrastructure;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class PlayerCoinsCollector : MonoBehaviour, IPunObservable
    {
        private PhotonView _photonView;
        
        public int CoinsCount { get; private set; }

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            
            AddObservable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin) == false) 
                return;
            
            CoinsCount++;
            
            PhotonNetwork.Destroy(coin.gameObject);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(CoinsCount);
            else
                CoinsCount = (int)stream.ReceiveNext();
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}