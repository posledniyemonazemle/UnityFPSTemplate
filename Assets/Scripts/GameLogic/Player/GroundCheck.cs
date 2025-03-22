using UnityEngine;

namespace Game.Player
{
    public class GroundCheck : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }
        
        [SerializeField] private float _rayLength;
        [SerializeField] private LayerMask _groundLayer;

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            IsGrounded = Physics.Raycast(this.transform.position, Vector3.down, _rayLength, _groundLayer);
        }
    }
}