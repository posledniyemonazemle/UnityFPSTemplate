using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerCrouch : MonoBehaviour
    {
        public bool IsCrouching { get; private set; }
        
        [SerializeField] private Transform _cameraHolder;
        [SerializeField] private float _crouchDuration;
        [SerializeField] private Vector3 _cameraTargetPosition;
        [SerializeField] private Collider _standCollider;
        [SerializeField] private Collider _crouchCollider;
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private float _playerHeight;
        
        private bool _isCrouchingCoroutineRunning;
        private Vector3 _cameraInitialPosition;
        private PlayerMove _playerMove;
        private GroundCheck _groundCheck;

        public void Initialize()
        {
            _playerMove = this.GetComponent<PlayerMove>();
            _groundCheck = this.GetComponent<GroundCheck>();
            _cameraInitialPosition = _cameraHolder.localPosition;
        }

        public void GetCrouchInput(InputAction.CallbackContext context)
        {
            if (context.performed && !_isCrouchingCoroutineRunning && _groundCheck.IsGrounded)
            {
				if (CheckObstacleOnTop()) return;
				
                if (!IsCrouching)
                {
                    StartCoroutine(SmoothCrouching(_cameraTargetPosition));
                    _playerMove.SetMoveSpeed(_crouchSpeed);
                }
                else if (IsCrouching)
                {
                    StartCoroutine(SmoothCrouching(_cameraInitialPosition));
                    _playerMove.ResetMoveSpeed();
                }
                
                IsCrouching = !IsCrouching;
                _standCollider.enabled = !IsCrouching;
                _crouchCollider.enabled = IsCrouching;
            }
        }

        private bool CheckObstacleOnTop()
        {
            return Physics.Raycast(this.transform.position, this.transform.up, _playerHeight * 0.5f);
        }

        private IEnumerator SmoothCrouching(Vector3 targetPos)
        {
            _isCrouchingCoroutineRunning = true;

            Vector3 startPos = _cameraHolder.localPosition;
            float time = 0;

            while (time < _crouchDuration)
            {
                time += Time.deltaTime;
                float t = time / _crouchDuration;

                _cameraHolder.localPosition = Vector3.Lerp(startPos, targetPos, t);

                yield return null;
            }

            _isCrouchingCoroutineRunning = false;
        }
    }
}