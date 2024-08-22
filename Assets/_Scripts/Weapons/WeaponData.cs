using UnityEngine;

namespace PhotonTest.Weapons 
{   
    public abstract class WeaponData : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }

        [Tooltip("The local position of the WeaponAttachmentPoint which it takes when the player looks up on maximum")]
        [field: SerializeField] public Vector3 UpperPointPosition { get; private set; }

        [Tooltip("The local position of the WeaponAttachmentPoint which it takes when the player looks down on maximum")]
        [field: SerializeField] public Vector3 LowerPointPosition { get; private set; }
    }
}