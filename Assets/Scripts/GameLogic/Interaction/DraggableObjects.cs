using Game.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Interaction
{
    public class DraggableObjects : MonoBehaviour
    {
        [SerializeField] private Joint _joint;
        [SerializeField] private float _pickUpDrag = 15f;
        [SerializeField] private float _throwForce = 10f;
        [SerializeField] private Collider[] _playerColliders = new Collider[2];

        private Rigidbody _targetRB;
        private Camera _mainCamera;
        private RaycastInteraction _raycastInteraction;
        private Collider _targetCollider;
        private float _targetDefaultDrag;

        private const string DraggableTag = "Draggable";

        public void Initialize()
        {
            _mainCamera = Camera.main;
            _raycastInteraction = _mainCamera.GetComponent<RaycastInteraction>();
        }

        public void GetInteractInput(InputAction.CallbackContext context)
        {
            if (context.performed) PickUp();
            else if (context.canceled) Drop(false);
        }
        
        public void GetSecondInteractInput(InputAction.CallbackContext context)
        {
            if (context.performed) Drop(true);
        }

        private void PickUp()
        {
            if (_raycastInteraction.CheckRaycast(DraggableTag))
            {
                _targetRB = _raycastInteraction.TargetedObject.GetComponent<Rigidbody>();
                _targetCollider = _raycastInteraction.TargetedObject.GetComponent<Collider>();

                foreach (Collider collider in _playerColliders) Physics.IgnoreCollision(_targetCollider, collider);
                
                _joint.connectedBody = _targetRB;
                _targetDefaultDrag = _targetRB.linearDamping;
                _targetRB.linearDamping = _pickUpDrag;
            }
        }

        private void Drop(bool isThrow)
        {
            if (_targetRB == null) return;
            
            foreach (Collider collider in _playerColliders) Physics.IgnoreCollision(_targetCollider, collider, false);
            
            _joint.connectedBody = null;
            _targetRB.linearDamping = _targetDefaultDrag;

            if (isThrow) _targetRB.AddForce(_mainCamera.transform.forward * _throwForce, ForceMode.Impulse);

            _targetRB = null;
        }
    }
}