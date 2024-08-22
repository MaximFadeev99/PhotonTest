using PhotonTest.Weapons.Bullets;
using UnityEngine;

namespace PhotonTest.Weapons.Firearms 
{
    [CreateAssetMenu(fileName = "NewRangedWeaponData", menuName = "ProjectData/RangedWeaponData", order = 51)]
    public class RangedWeaponData : WeaponData
    {
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public int MagazineCapacity { get; private set; }
    }
}