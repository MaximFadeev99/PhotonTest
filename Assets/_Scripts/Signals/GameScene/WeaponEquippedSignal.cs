using PhotonTest.Weapons;

namespace PhotonTest.Signals 
{
    public class WeaponEquippedSignal
    {
        public readonly int PlayerViewId;
        public readonly Weapon EquippedWeapon;
        public readonly int EquippedWeaponIndex;

        public WeaponEquippedSignal(int playerViewId, Weapon equippedWeapom, int equippedWeaponIndex)
        {
            PlayerViewId = playerViewId;
            EquippedWeapon = equippedWeapom;
            EquippedWeaponIndex = equippedWeaponIndex;
        }
    }
}