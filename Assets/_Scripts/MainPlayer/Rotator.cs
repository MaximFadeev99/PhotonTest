using System;
using UnityEngine;

namespace PhotonTest.MainPlayer 
{
    public class Rotator
    {
        public readonly float XRotationLimit;
        private readonly Transform _playerTransform;
        private readonly Transform _cameraTransform;
        private readonly float _rotationSpeedModifier;

        private float _targetXRotation;
        private Quaternion _targetQuaternion;
        private Quaternion _thresholdQuaternion;

        public Action<float> CameraMovedOnX;

        internal Rotator(Transform playerTransform, Transform cameraTransform, float rotationSpeedModifier, float xRotationLimit) 
        {
            _playerTransform = playerTransform;
            _cameraTransform = cameraTransform;
            _rotationSpeedModifier = rotationSpeedModifier;
            XRotationLimit = xRotationLimit;
        }

        internal void Rotate(Vector2 rotationInput) 
        {           
            _playerTransform.Rotate(Vector3.up, rotationInput.x * _rotationSpeedModifier * Time.deltaTime);

            _targetXRotation = _cameraTransform.localRotation.eulerAngles.x -
                rotationInput.y * _rotationSpeedModifier * Time.deltaTime;
            _targetQuaternion = Quaternion.Euler(_targetXRotation, _cameraTransform.localEulerAngles.y,
                _cameraTransform.localRotation.eulerAngles.z);
            _thresholdQuaternion = Quaternion.Euler(0f, _cameraTransform.localEulerAngles.y,
                _cameraTransform.localEulerAngles.z);

            if (Quaternion.Angle(_targetQuaternion, _thresholdQuaternion) < XRotationLimit)
            {
                _cameraTransform.localRotation = _targetQuaternion;
                CameraMovedOnX?.Invoke(_cameraTransform.localEulerAngles.x);
            }
        }
    }
}