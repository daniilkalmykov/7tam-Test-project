using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI.Texts
{
    [RequireComponent(typeof(TMP_Text), typeof(PhotonView))]
    public sealed class NickNameText : MonoBehaviour, IPunObservable
    {
        private TMP_Text _tmpText;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _tmpText = GetComponent<TMP_Text>();
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(_tmpText.text);
            else
                _tmpText.text = (string)stream.ReceiveNext();
        }        
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}