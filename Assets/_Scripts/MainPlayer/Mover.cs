using UnityEngine;

namespace PhotonTest.MainPlayer 
{
    internal class Mover
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly float _speedModifier;

        internal Mover(Rigidbody rigidbody, Transform transform, float speedModifier)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _speedModifier = speedModifier;
        }

        internal void Move(Vector2 directionInput)
        {
            _rigidbody.velocity = directionInput.y * _speedModifier * _transform.forward;

            if (directionInput.y != 0)
                return;

            _rigidbody.velocity = directionInput.x * _speedModifier * _transform.right;
        }
    }
}