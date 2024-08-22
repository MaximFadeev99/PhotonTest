using UnityEngine;
using PhotonTest.Utilities;
using Zenject;
using System;

namespace PhotonTest.Weapons.Bullets 
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class Bullet : MonoBehaviour, IMonoBehaviourPoolElement
    {
        [SerializeField] private float _damageModifier;
        [SerializeField] private float _shotImpulse;

        private BulletDecalManager _bulletDecalManager;
        private Rigidbody _rigidbody;
        private bool _isFlying = false;
        private float _timer;
        private Vector3 _currentFireline;

        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }
        public string PlayerNickname { get; private set; }

        [Inject]
        private void Construct(BulletDecalManager bulletDecalManager) 
        { 
            _bulletDecalManager = bulletDecalManager;
        }

        public void Awake()
        {
            GameObject = gameObject;
            Transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetPlayerNickname(string nickname) 
        {
            PlayerNickname = nickname;
        }

        public void StartFlying(Quaternion localRotation, Vector3 localPosition, Vector3 fireline)
        {
            if (PlayerNickname == string.Empty)
                throw new Exception($"Bullets must not be shot until {nameof(PlayerNickname)} is not set");

            GameObject.SetActive(true);
            Transform.SetLocalPositionAndRotation(localPosition, localRotation);
            _currentFireline = fireline;
            _rigidbody.velocity = Vector3.zero;
            Transform.SetParent(null);
            _isFlying = true;
        }

        private void Update()
        {
            if (_isFlying == false)
                return;

            _rigidbody.velocity = _currentFireline * _shotImpulse;
            _timer += Time.deltaTime;

            if (_timer > 5f)
                EndFlying();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _bulletDecalManager.PlaceDecal(collision.contacts[0].point, collision.contacts[0].normal,
                PlayerNickname);
            EndFlying();
        }

        protected virtual void EndFlying()
        {
            GameObject.SetActive(false);
            _isFlying = false;
            _timer = 0f;
        }
    }
}