using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Effects
{
    public class ItemSway : MonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private float _swayClamp;

        private Vector2 _look;

        public void GetLookInput(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            _look.x = Mathf.Clamp(_look.x, -_swayClamp, _swayClamp);
            _look.y = Mathf.Clamp(_look.y, -_swayClamp, _swayClamp);

            Quaternion target = Quaternion.Euler(_look.y, -_look.x, 0f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, target, Time.deltaTime * _smooth);
        }
    }
}