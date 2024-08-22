using UnityEngine;

namespace PhotonTest.Weapons 
{
    public class DecalPlacedSignal
    {
        public Vector3 WorldPosition { get; private set; }
        public Vector3 SurfaceNormal { get; private set; }
        public string PlayerNickname { get; private set; }

        public DecalPlacedSignal(Vector3 worldPosition, Vector3 surfaceNormal, string playerNickname)
        {
            WorldPosition = worldPosition;
            SurfaceNormal = surfaceNormal;
            PlayerNickname = playerNickname;
        }
    }
}