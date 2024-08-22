using UnityEngine;

namespace PhotonTest.Weapons
{
    public class Crosshair
    {
        private readonly RaycastHit[] _hits = new RaycastHit[1];
        private readonly Vector3 _screenCenter;
        private readonly Camera _camera;

        private Ray _screenCenterRay;
        private float _attackRange;

        public Crosshair(Camera camera)
        {
            _camera = camera;
            _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        }

        public void OnWeaponEquipped(float attackRange) 
        {
            _attackRange = attackRange;
        }

        public Vector3 GetPositionInAim() 
        {
            _hits[0] = new RaycastHit();
            _screenCenterRay = _camera.ScreenPointToRay(_screenCenter);
            Physics.RaycastNonAlloc(_screenCenterRay, _hits, _attackRange);

            return _hits[0].transform == null ? _screenCenterRay.origin + _screenCenterRay.direction * _attackRange:
                _hits[0].point;
        }
    }
}