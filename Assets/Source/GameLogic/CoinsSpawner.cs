using System.Collections.Generic;
using Infrastructure;
using Photon.Pun;
using UnityEngine;

namespace GameLogic
{
    public sealed class CoinsSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _prefab;
        [SerializeField] private int _count;
        [SerializeField] private List<Transform> _spawnPoints;

        private void Start()
        {
            while (_count > _spawnPoints.Count)
                _count--;
        }
        
        public void Spawn()
        {
            for (var i = 0; i < _count; i++)
            {
                var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                PhotonNetwork.InstantiateRoomObject(_prefab.name, spawnPoint.position, Quaternion.identity);

                _spawnPoints.Remove(spawnPoint);
            }
        }
    }
}