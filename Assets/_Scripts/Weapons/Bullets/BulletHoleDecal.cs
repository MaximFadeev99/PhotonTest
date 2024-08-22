using PhotonTest.Utilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class BulletHoleDecal : MonoBehaviour, IMonoBehaviourPoolElement
{
    public GameObject GameObject { get; private set; }
    public Transform Transform { get; private set; }

    public void Awake()
    {
        GameObject = gameObject;
        Transform = transform;
    }

    private void OnDisable()
    {
        Transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}