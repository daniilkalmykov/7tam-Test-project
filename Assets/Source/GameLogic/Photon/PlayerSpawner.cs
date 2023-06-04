using Photon.Pun;
using UnityEngine;

namespace GameLogic.Photon
{
    public sealed class PlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _prefab;
        
        public void SpawnPlayer()
        {
            PhotonNetwork.Instantiate(_prefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}