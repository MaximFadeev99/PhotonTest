using PhotonTest.Signals;
using PhotonTest.Weapons;
using TMPro;
using UnityEngine;
using Zenject;

namespace PhotonTest.UI 
{
    internal class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentAmmoField;
        [SerializeField] private TMP_Text _maxAmmoField;

        private RangedWeapon _equippedWeapon;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<WeaponEquippedSignal>(OnWeaponEquipped);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<WeaponEquippedSignal>(OnWeaponEquipped);
        }

        private void OnAmmoCountChanged(int ammoLeftInMagazine)
        {
            _currentAmmoField.text = ammoLeftInMagazine.ToString();
            _currentAmmoField.color = ammoLeftInMagazine == 0 ? Color.red : Color.white;
        }

        private void OnWeaponEquipped(WeaponEquippedSignal signal)
        {
            if (_equippedWeapon != null)
            {
                _equippedWeapon.Fired -= OnAmmoCountChanged;
                _equippedWeapon.Reloaded -= OnAmmoCountChanged;
            }

            RangedWeapon newRangedWeapon = signal.EquippedWeapon as RangedWeapon;

            if (signal.EquippedWeapon == null || newRangedWeapon == null)
                return;

            _equippedWeapon = newRangedWeapon;
            _equippedWeapon.Fired += OnAmmoCountChanged;
            _equippedWeapon.Reloaded += OnAmmoCountChanged;
            _maxAmmoField.text = _equippedWeapon.RangedWeaponData.MagazineCapacity.ToString();
            OnAmmoCountChanged(_equippedWeapon.CurrentAmmoCount);
        }
    }
}