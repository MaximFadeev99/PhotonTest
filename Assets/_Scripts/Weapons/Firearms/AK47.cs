using UnityEngine;
using PhotonTest.Weapons.Bullets;
using PhotonTest.Utilities;

namespace PhotonTest.Weapons.Firearms
{
    public class AK47 : RangedWeapon
    {
        public override void Use() 
        {
            if (CurrentAmmoCount == 0 || IsBeingReloaded)
                return;

            Animator.SetBool(AnimatorParameters.IsShootingHash, true);
        }

        public void Fire()
        {
            if (CurrentAmmoCount == 0 || IsBeingReloaded) 
            {
                Animator.SetBool(AnimatorParameters.IsShootingHash, false);
                return;
            }

            Bullet idleBullet = BulletPool.GetIdleElement();
            Vector3 fireline = (PositionInAim - AttackPoint.position).normalized;

            idleBullet.SetPlayerNickname(PlayerNickname);
            idleBullet.Transform.SetParent(BulletContainer);
            idleBullet.StartFlying(Quaternion.identity, Vector3.zero, fireline);
            Fired?.Invoke(--CurrentAmmoCount);
        }
    }
}