using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerSprint : MonoBehaviour
    {
        [SerializeField] private float _sprintSpeed;

        private PlayerMove _playerMove;
        private PlayerCrouch _playerCrouch;

        public void Initialize()
        {
            _playerMove = GetComponent<PlayerMove>();
            _playerCrouch = GetComponent<PlayerCrouch>();
        }

        public void GetSprintInput(InputAction.CallbackContext context)
        {
            if (_playerCrouch.IsCrouching) return;
            
            if (context.performed) _playerMove.SetMoveSpeed(_sprintSpeed);
            else if (context.canceled) _playerMove.ResetMoveSpeed();
        }
    }
}