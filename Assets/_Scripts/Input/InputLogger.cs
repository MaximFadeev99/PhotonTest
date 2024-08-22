using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace PhotonTest.Input
{
    public class InputLogger : ITickable, IDisposable
    {
        private const string MouseName = "Mouse";

        private readonly PlayerInputAsset _playerInputAsset;
        private readonly InputSystemUIInputModule _inputModule;
        private readonly int _mouseID;

        private Vector2 _currentDirectionInput;
        private Vector2 _currentRotationInput;
        private bool _isPointerOverUI;

        public Action FirePressed;
        public Action FireReleased;
        public Action AimPressed;
        public Action ReloadPressed;
        public Action<Vector2> MovementRegistered;
        public Action<Vector2> RotationRegistered;
        public Action<int> ChangeWeaponPressed;

        public InputLogger(InputSystemUIInputModule inputModule) 
        {
            _inputModule = inputModule;
            _mouseID = InputSystem.devices.First(device => device.name == MouseName).deviceId;
            _playerInputAsset = new();
            _playerInputAsset.Enable();
            _playerInputAsset.PCMap.Enable();
            _playerInputAsset.PCMap.Fire.started += OnFirePressedDown;
            _playerInputAsset.PCMap.Aim.started += OnAimPressed;
            _playerInputAsset.PCMap.Reload.started += OnReloadPressed;
            _playerInputAsset.PCMap.ChangeWeapon.performed += OnChangeWeaponPressed;
        }

        public void Tick()
        {
            ReadVector2Input(_playerInputAsset.PCMap.DirectionInput, ref _currentDirectionInput, MovementRegistered);
            ReadVector2Input(_playerInputAsset.PCMap.RotationInput, ref _currentRotationInput, RotationRegistered);
            _isPointerOverUI = _inputModule.IsPointerOverGameObject(_mouseID);
        }

        public void Dispose()
        {
            _playerInputAsset.PCMap.Fire.started -= OnFirePressedDown;
            _playerInputAsset.PCMap.Aim.started -= OnAimPressed;
            _playerInputAsset.PCMap.Reload.started -= OnReloadPressed;
            _playerInputAsset.PCMap.ChangeWeapon.performed -= OnChangeWeaponPressed;
            _playerInputAsset.Disable();
            _playerInputAsset.Dispose();
        }

        //WARNING: Do not name this callback methods like "On + InputAction". It leads to unexplained firing of MissingMethodException
        //See forum: https://forum.unity.com/threads/getting-a-missingmethodexception-for-a-method-im-not-currently-using.1469819/
        private void OnFirePressedDown(CallbackContext context) 
        {
            if (_isPointerOverUI)
                return;

            FirePressed?.Invoke();
            _ = WaitFireUntilReleased();
        }

        private async UniTaskVoid WaitFireUntilReleased() 
        {
            await UniTask.WaitUntil(() => _playerInputAsset.PCMap.Fire.IsPressed() == false);
            FireReleased?.Invoke();
        }

        private void OnAimPressed(CallbackContext context) 
        {
            AimPressed?.Invoke();
        }

        private void OnReloadPressed(CallbackContext context) 
        {
            ReloadPressed?.Invoke();
        }

        private void OnChangeWeaponPressed(CallbackContext context) 
        {
            Vector2 deltaScroll = context.ReadValue<Vector2>();
            ChangeWeaponPressed?.Invoke(deltaScroll.y < 0 ? -1 : 1);
        }

        private void ReadVector2Input(InputAction targetAction, ref Vector2 memberVariable, Action<Vector2> callback) 
        {
            Vector2 currentValue = targetAction.ReadValue<Vector2>();

            if (currentValue == Vector2.zero && memberVariable == Vector2.zero)
                return;

            memberVariable = currentValue;
            callback?.Invoke(memberVariable);
        }
    }
}