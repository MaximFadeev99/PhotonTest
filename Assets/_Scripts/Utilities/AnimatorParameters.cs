using UnityEngine;

namespace PhotonTest.Utilities
{
    public static class AnimatorParameters
    {
        public static readonly int ShootHash = Animator.StringToHash("Shoot");
        public static readonly int ReloadHash = Animator.StringToHash("Reload");
        public static readonly int IsShootingHash = Animator.StringToHash("IsShooting");
    }
}