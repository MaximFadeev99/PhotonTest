using System;
using UnityEngine;

namespace PhotonTest.Weapons
{
    public class WeaponAttachmentPoint
    {
        private readonly Transform _crosshairTransform;
        private readonly Transform _transform;
        private readonly float _xRotationLimit;

        private Vector3 _lowerPointPosition;
        private Vector3 _upperPointPosition;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private Quaternion _adjustedRotation;
        private Transform _firePoint;

        public WeaponAttachmentPoint (Transform transform, float xRotationLimit, Transform crosshairTransform)
        {
            _transform = transform;
            _initialPosition = _transform.localPosition;
            _initialRotation = _transform.localRotation;
            _xRotationLimit = xRotationLimit;
            _crosshairTransform = crosshairTransform;
        }

        public void OnWeaponEquipped(ref Action<float> cameraXRotationChangedEvent, Vector3 lowerPointPosition,
            Vector3 upperPointPosition, Transform firePoint)
        {
            cameraXRotationChangedEvent += OnXRotationChanged;
            _lowerPointPosition = lowerPointPosition;
            _upperPointPosition = upperPointPosition;
            _firePoint = firePoint;
        }

        public void OnWeaponDeequipped(ref Action<float> cameraXRotationChangedEvent)
        {
            cameraXRotationChangedEvent -= OnXRotationChanged;
            Reset();
        }

        private void OnXRotationChanged(float currentXRotation)
        {
            AdjustPosition(currentXRotation);
            AdjustRotation();
        }

        private void AdjustPosition(float currentXRotation)
        {
            if (currentXRotation > 0f && currentXRotation < _xRotationLimit)
            {
                _transform.localPosition = Vector3.Lerp
                    (_initialPosition, _lowerPointPosition, currentXRotation / _xRotationLimit);
            }
            else if (currentXRotation < 360f && currentXRotation > 360f - _xRotationLimit)
            {
                _transform.localPosition = Vector3.Lerp
                    (_initialPosition, _upperPointPosition, (360f - currentXRotation) / _xRotationLimit);
            }
            else
            {
                _transform.localPosition = _initialPosition;
            }
        }

        private void AdjustRotation() 
        {
            _adjustedRotation = Quaternion.LookRotation
                (_crosshairTransform.position - _firePoint.position + _firePoint.localPosition);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _adjustedRotation, 0.1f);

            //More detailed way of adjusting parent's rotation based on the desired rotation of its child object
            //The following lines of code are identicl to the above ones

            //Vector3 firePointLookDirection = _crosshairTransform.position - _firePoint.position;
            //Vector3 parentLookDirection = firePointLookDirection + _firePoint.localPosition;
            //Quaternion parenLookRotation = Quaternion.LookRotation(parentLookDirection);
            //_transform.rotation = parenLookRotation;

        }

        private void Reset()
        {
            _transform.SetPositionAndRotation(_initialPosition, _initialRotation);
            _lowerPointPosition = Vector3.zero;
        }
    }
}