using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public Rigidbody RB { get; private set; }
        
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _groundDrag;
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _airMultiplier;

        private float _moveSpeed;
        private Vector2 _move;
        private Vector3 _moveDirection;
        private PlayerCrouch _playerCrouch;
        private GroundCheck _groundCheck;
        
        public void Initialize()
        {
            RB = this.GetComponent<Rigidbody>();
            RB.freezeRotation = true;
            _moveSpeed = _walkSpeed;
            _playerCrouch = this.GetComponent<PlayerCrouch>();
            _groundCheck = this.GetComponent<GroundCheck>();
        }

        private void Update()
        {
            SpeedControl();
            HandleDrag();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        public void GetMovementInput(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        private void MovePlayer()
        {
            _moveDirection = _orientation.forward * _move.y + _orientation.right * _move.x;
            if (_groundCheck.IsGrounded)
                RB.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Force);
            else
                RB.AddForce(_moveDirection.normalized * _moveSpeed * _airMultiplier, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(RB.linearVelocity.x, 0f, RB.linearVelocity.z);

            if (flatVel.magnitude > _moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                RB.linearVelocity = new Vector3(limitedVel.x, RB.linearVelocity.y, limitedVel.z);
            }
        }

        private void HandleDrag()
        {
            RB.linearDamping = _groundCheck.IsGrounded ? _groundDrag : 0f;
        }

        public void SetMoveSpeed(float speed)
        {
            _moveSpeed = speed;
        }

        public void ResetMoveSpeed()
        {
            _moveSpeed = _walkSpeed;
        }
    }
}