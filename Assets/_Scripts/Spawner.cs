using PhotonTest.MainPlayer;
using UnityEngine;
using Photon.Pun;
using Zenject;
using PhotonTest.Input;

namespace PhotonTest 
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnZoneCenter;
        [SerializeField] private float _spawnZoneRadius;
        [SerializeField] private Transform _crosshairTransform;
        [SerializeField] private Transform _firstPersonCamera;   

        private SignalBus _signalBus;
        private InputLogger _inputLogger;

        public void Initialize(SignalBus signalBus, InputLogger inputLogger) 
        {
            _signalBus = signalBus;
            _inputLogger = inputLogger;
        }

        public Player SpawnPlayer(string nickname) 
        {
            Player newPlayer = PhotonNetwork
                .Instantiate(_playerPrefab.name, GetSpawnPosition(), Quaternion.identity)
                .GetComponent<Player>();
            newPlayer.Initialize(_inputLogger, _signalBus, nickname, _crosshairTransform, _firstPersonCamera);
            
            return newPlayer;
        }

        private Vector3 GetSpawnPosition() 
        {
            return _spawnZoneCenter.position;
        }
    }
}