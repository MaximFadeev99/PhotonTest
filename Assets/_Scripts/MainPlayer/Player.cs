using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using PhotonTest.Input;
using PhotonTest.Signals;
using PhotonTest.Utilities;
using PhotonTest.Weapons;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PhotonTest.MainPlayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _availableWeapons;
        [SerializeField] private Vector3 _cameraLocalPosition;

        [Header("Rotation")]
        [SerializeField] private float _rotationSpeedModifier = 10f;
        [SerializeField] private float _xRotationLimit = 45f;

        [Header("Movement")]
        [SerializeField] private float _movementSpeedModifier = 2f;

        [Space]
        [SerializeField] private Transform _weaponAttachementPointTransform;

        private Transform _firstPersonCamera;
        private InputLogger _inputLogger;
        private Transform _transform;
        private PhotonView _photonView;
        private Mover _mover;
        private WeaponAttachmentPoint _weaponAttachementPoint;
        private Crosshair _crosshair;
        private Transform _crosshairTransform;
        private SignalBus _signalBus;
        private Rigidbody _rigidbody;
        private bool _isFiringMulptipleShots = false;
        private Vector2 _rotationInput;

        public Collider Collider { get; private set; }
        public Rotator Rotator { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public RangedWeapon EquippedRangedWeapon { get; private set; }
        public string Nickname { get; private set; }
        public int ViewID => _photonView.ViewID;

        public void Initialize(InputLogger inputLogger, SignalBus signalBus, string nickname,
            Transform crosshairTransform, Transform firstPersonCamera)
        {
            _inputLogger = inputLogger;
            _signalBus = signalBus;
            _crosshairTransform = crosshairTransform;
            _firstPersonCamera = firstPersonCamera;
            Nickname = nickname;
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _photonView = GetComponent<PhotonView>();
            Rotator = new(_transform, _firstPersonCamera, _rotationSpeedModifier, _xRotationLimit);
            _mover = new(_rigidbody, _transform, _movementSpeedModifier);
            _weaponAttachementPoint = new(_weaponAttachementPointTransform, Rotator.XRotationLimit, _crosshairTransform);
            _crosshair = new(_firstPersonCamera.GetComponentInChildren<Camera>());

            foreach (Weapon weapon in _availableWeapons)
                weapon.Awake();

            if (_photonView.IsMine == false)
                return;

            SetSubscriptions();
            EquipWeapon(_availableWeapons[0]);
            _firstPersonCamera.transform.SetParent(_transform);
            _firstPersonCamera.SetLocalPositionAndRotation(_cameraLocalPosition, Quaternion.Euler(Vector3.zero));

            Hashtable newHashable = PhotonNetwork.LocalPlayer.CustomProperties;
            newHashable[CustomProperties.ViewID] = _photonView.ViewID;
            PhotonNetwork.LocalPlayer.SetCustomProperties(newHashable);
        }

        public void InitializeAsClone(string nickname, int weaponIndex) 
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _photonView = GetComponent<PhotonView>();
            Nickname = nickname;

            foreach (Weapon weapon in _availableWeapons)
                weapon.Awake();

            EquipWeapon(weaponIndex);
        }

        public void EquipWeapon(Weapon weaponToEquip)
        {
            if (_availableWeapons.Contains(weaponToEquip) == false) 
            {
                CustomLogger.Log($"{nameof(Player)}, nickname {Nickname}", $"You are trying to equip " +
                    $"{weaponToEquip.name} that has not been preliminarily added to {nameof(_availableWeapons)} list",
                    MessageTypes.Error);
                return;
            }

            SetWeapon(weaponToEquip);
        }

        public void EquipWeapon(int weaponIndex) 
        {
            if (weaponIndex < 0 || weaponIndex > _availableWeapons.Count - 1) 
            {
                CustomLogger.Log($"{nameof(Player)}, nickname {Nickname}", $"You are trying to equip " +
                    $"weapon of index {weaponIndex}, but this index is either below 0 or more than the current count of " +
                    $"{nameof(_availableWeapons)} list",
                    MessageTypes.Error);
                return;
            }

            SetWeapon(_availableWeapons[weaponIndex]);
        }

        private void SetWeapon(Weapon weaponToEquip) 
        {
            if (EquippedWeapon != null)
                EquippedWeapon.gameObject.SetActive(false);

            EquippedWeapon = weaponToEquip;
            EquippedWeapon.gameObject.SetActive(true);

            if (EquippedWeapon is RangedWeapon rangedWeapon)
            {
                EquippedRangedWeapon = rangedWeapon;
                EquippedRangedWeapon.SetPlayerNickname(Nickname);
            }

            if (_photonView.IsMine == false)
                return;

            _weaponAttachementPoint.OnWeaponEquipped(ref Rotator.CameraMovedOnX, EquippedWeapon.WeaponData.LowerPointPosition,
                EquippedWeapon.WeaponData.UpperPointPosition, weaponToEquip.AttackPoint);
            _crosshair.OnWeaponEquipped(EquippedWeapon.WeaponData.AttackRange);

            int equippedWeaponIndex = _availableWeapons.IndexOf(EquippedWeapon);
            Hashtable newHashtable = PhotonNetwork.LocalPlayer.CustomProperties;

            newHashtable[CustomProperties.WeaponIndex] = equippedWeaponIndex;
            PhotonNetwork.LocalPlayer.SetCustomProperties(newHashtable);
            _signalBus.Fire(new WeaponEquippedSignal(_photonView.ViewID, EquippedWeapon, 
                _availableWeapons.IndexOf(EquippedWeapon)));
        }

        private void SetSubscriptions()
        {
            _inputLogger.AimPressed += Aim;
            _inputLogger.FirePressed += Fire;
            _inputLogger.FireReleased += OnFireReleased;
            _inputLogger.ReloadPressed += Reload;
            _inputLogger.MovementRegistered += Move;
            _inputLogger.RotationRegistered += Rotate;
            _inputLogger.ChangeWeaponPressed += ChangeEquippedWeapon;
        }

        private void RemoveSubscriptions()
        {
            if (_photonView == null || _photonView.IsMine == false)
                return;

            _inputLogger.AimPressed -= Aim;
            _inputLogger.FirePressed -= Fire;
            _inputLogger.FireReleased -= OnFireReleased;
            _inputLogger.ReloadPressed -= Reload;
            _inputLogger.MovementRegistered -= Move;
            _inputLogger.RotationRegistered -= Rotate;
            _inputLogger.ChangeWeaponPressed -= ChangeEquippedWeapon;
        }

        private void Fire() 
        {
            if (_photonView.IsMine == false || EquippedWeapon == null)
                return;

            if (EquippedRangedWeapon != null && EquippedRangedWeapon.ShootingMode == ShootingModes.SingleShots) 
            {
                Vector3 positionInAim = _crosshair.GetPositionInAim();
                EquippedRangedWeapon.UpdatePositionInAim(positionInAim);
                EquippedRangedWeapon.Use();
                return;
            }

            if (EquippedRangedWeapon != null && EquippedRangedWeapon.ShootingMode == ShootingModes.MultipleShots) 
            {
                Vector3 positionInAim = _crosshair.GetPositionInAim();
                EquippedRangedWeapon.UpdatePositionInAim(positionInAim);
                EquippedRangedWeapon.Use();
                _isFiringMulptipleShots = true;
                _ = KeepUpdatingPositionInAim();
            }
        }

        private void OnFireReleased() 
        {
            _isFiringMulptipleShots = false;
            EquippedRangedWeapon.StopFire();
        }

        private async UniTaskVoid KeepUpdatingPositionInAim() 
        {
            while (_isFiringMulptipleShots) 
            {
                EquippedRangedWeapon.UpdatePositionInAim(_crosshair.GetPositionInAim());
                await UniTask.Yield();
            }
        }

        private void Aim() 
        {
            
        }

        private void Reload() 
        {
            if (EquippedRangedWeapon != null)
                EquippedRangedWeapon.Reload();
        }

        private void Move(Vector2 directionInput) 
        {
            if (_photonView.IsMine == false)
                return;

            _mover.Move(directionInput);
        }

        private void Rotate(Vector2 rotationInput)
        {
            if (_photonView.IsMine == false)
                return;

            _rotationInput = rotationInput;
        }

        private void ChangeEquippedWeapon(int deltaScroll) 
        {
            if (_photonView.IsMine == false || EquippedRangedWeapon.IsBeingReloaded)
                return;

            int nextWeaponPosition = _availableWeapons.IndexOf(EquippedWeapon) + deltaScroll;

            if (nextWeaponPosition < 0)
                nextWeaponPosition = _availableWeapons.Count - 1;

            if (nextWeaponPosition > _availableWeapons.Count - 1)
                nextWeaponPosition = 0;

            EquipWeapon(_availableWeapons[nextWeaponPosition]);
        }

        private void LateUpdate()
        {
            if (_rotationInput != Vector2.zero)
            {
                Rotator.Rotate(_rotationInput);
                _rotationInput = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            RemoveSubscriptions();
        }
    }
}