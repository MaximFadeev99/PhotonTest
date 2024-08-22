using PhotonTest.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PhotonTest.Weapons.Bullets 
{
    public class BulletDecalManager : MonoBehaviour
    {
        [SerializeField] private BulletHoleDecal _bulletHoleDecalPrefab;
        [SerializeField] private int _maxDecalCount = 15;

        private readonly List<BulletHoleDecal> _activeBulletDecals = new();

        private MonoBehaviourPool<BulletHoleDecal> _bulletHoleDecalPool;
        private Transform _transform;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _transform = transform;
            _bulletHoleDecalPool = new(_bulletHoleDecalPrefab, _transform, _maxDecalCount);
        }

        public void PlaceDecal(Vector3 worldPosition, Vector3 surfaceNormal, string playerNickname, bool shallFireEvent = true) 
        {
            TryDisableOldDecal();

            BulletHoleDecal idleBulletHole = _bulletHoleDecalPool.GetIdleElement();

            idleBulletHole.Transform.SetPositionAndRotation(worldPosition, Quaternion.LookRotation(surfaceNormal));
            idleBulletHole.GameObject.SetActive(true);
            _activeBulletDecals.Add(idleBulletHole);

            if (shallFireEvent) 
                _signalBus.Fire(new DecalPlacedSignal(worldPosition, surfaceNormal, playerNickname));
        }

        private void TryDisableOldDecal() 
        {
            if (_activeBulletDecals.Count < _maxDecalCount)
                return;

            _activeBulletDecals[0].GameObject.SetActive(false);
            _activeBulletDecals.RemoveAt(0);
        }       
    }
}