using UnityEngine;

namespace PhotonTest.Utilities
{
    public class DistanceMeasurer : MonoBehaviour
    {
        [SerializeField] private Transform _end1;
        [SerializeField] private Transform _end2;

        private void Update()
        {
            Debug.Log(Vector3.Distance(_end1.position, _end2.position));
        }
    }
}
