using PhotonTest.Utilities;
using PhotonTest.Weapons.Firearms;
using System;
using UnityEngine;
using Bullet = PhotonTest.Weapons.Bullets.Bullet;

namespace PhotonTest.Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        [field: SerializeField] protected Transform BulletContainer { get; private set; }
        [field: SerializeField] public RangedWeaponData RangedWeaponData { get; private set; }
        [field: SerializeField] public ShootingModes ShootingMode { get; private set; }

        protected string PlayerNickname { get; private set; }
        protected MonoBehaviourPool<Bullet> BulletPool { get; private set; }
        public bool IsBeingReloaded { get; private set; } = false;
        public override WeaponData WeaponData => RangedWeaponData;
        public int CurrentAmmoCount { get; protected set; } = 0;
        protected Vector3 PositionInAim { get; private set; }

        public Action<int> Fired;
        public Action<int> Reloaded;

        public override void Awake()
        {
            base.Awake();
            BulletPool = new(RangedWeaponData.BulletPrefab, BulletContainer);
            CurrentAmmoCount = RangedWeaponData.MagazineCapacity;
        }

        public override void Use()
        {
            if (IsOnCooldown || CurrentAmmoCount == 0 || IsBeingReloaded)
                return;

            Bullet idleBullet = BulletPool.GetIdleElement();
            Vector3 fireline = (PositionInAim - AttackPoint.position).normalized;

            idleBullet.SetPlayerNickname(PlayerNickname);
            idleBullet.Transform.SetParent(BulletContainer);
            idleBullet.StartFlying(Quaternion.identity, Vector3.zero, fireline);
            Animator.SetTrigger(AnimatorParameters.ShootHash);
            IsOnCooldown = true;
            Fired?.Invoke(--CurrentAmmoCount);
        }

        public void Reload()
        {
            if (IsBeingReloaded)
                return;

            IsBeingReloaded = true;
            Animator.SetTrigger(AnimatorParameters.ReloadHash);
        }

        public void EndReload()
        {
            IsBeingReloaded = false;
            CurrentAmmoCount = RangedWeaponData.MagazineCapacity;
            Reloaded?.Invoke(CurrentAmmoCount);
        }

        public void UpdatePositionInAim(Vector3 positionInAim)
        {
            PositionInAim = positionInAim;
        }

        public void StopFire()
        {
            if (ShootingMode == ShootingModes.SingleShots)
                return;

            Animator.SetBool(AnimatorParameters.IsShootingHash, false);
        }

        public void SetPlayerNickname(string nickName) 
        {
            PlayerNickname = nickName;
        }
    }

    public enum ShootingModes 
    {
        SingleShots, 
        MultipleShots
    }
}