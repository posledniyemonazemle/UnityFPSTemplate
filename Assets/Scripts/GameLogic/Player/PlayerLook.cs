using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerLook : MonoBehaviour
    {
        public float Sensitivity;

        [SerializeField] private Transform _orientation;
        [SerializeField] private float _minClampAngle;
        [SerializeField] private float _maxClampAngle;

        private float _xRotation;
        private float _yRotation;
        private Vector2 _look;

        public void GetLookInput(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Reset();
        }

        private void Reset()
        {
            _look = new Vector2(0f, 0f);
            _xRotation = 0f;
            _yRotation = 0f;
            this.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            _orientation.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void LateUpdate()
        {
            _yRotation += _look.x * Sensitivity;
            _xRotation -= _look.y * Sensitivity;
            _xRotation = Mathf.Clamp(_xRotation, _minClampAngle, _maxClampAngle);

            this.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            _orientation.transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
        }

        public Vector2 GetRotation()
        {
            return new Vector2(_yRotation, _xRotation);
        }
    }
}