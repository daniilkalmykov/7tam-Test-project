using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI.Texts
{
    [RequireComponent(typeof(TMP_Text), typeof(PhotonView))]
    public sealed class NickNameText : MonoBehaviour, IPunObservable, IPunInstantiateMagicCallback
    {
        private const int ParentId = 1;
        
        private TMP_Text _tmpText;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _tmpText = GetComponent<TMP_Text>();
            
            AddObservable();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
                stream.SendNext(_tmpText.text);
            else
                _tmpText.text = (string)stream.ReceiveNext();
        }        

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            transform.SetParent(PhotonView.Find(ParentId).transform);
        }
        
        private void AddObservable()
        {
            if (_photonView.ObservedComponents.Contains(this) == false)
                _photonView.ObservedComponents.Add(this);
        }
    }
}