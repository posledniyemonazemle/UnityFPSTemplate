using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;
        
        private bool _readyToJump;
        private Rigidbody _rb;
        private PlayerCrouch _playerCrouch;
        private GroundCheck _groundCheck;

        public void Initialize()
        {
            _rb = this.GetComponent<Rigidbody>();
            _groundCheck = this.GetComponent<GroundCheck>();
            _playerCrouch = this.GetComponent<PlayerCrouch>();
            _readyToJump = true;
        }

        public void GetJumpInput(InputAction.CallbackContext context)
        {
            if (_readyToJump && _groundCheck.IsGrounded && !_playerCrouch.IsCrouching)
            {
                _readyToJump = false;

                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
                _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

                Invoke(nameof(ResetJump), _jumpCooldown);
            }
        }

        private void ResetJump()
        {
            _readyToJump = true;
        }
    }
}