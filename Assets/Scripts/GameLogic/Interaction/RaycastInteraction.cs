using UnityEngine;

namespace Game.Player
{
    public class RaycastInteraction : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 2f;
        public GameObject TargetedObject { get; private set; }

        [SerializeField] private LayerMask _ignoreRaycastLayer;

        void Update()
        {
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, _maxDistance,
                ~_ignoreRaycastLayer)) TargetedObject = hit.collider.gameObject;
            else TargetedObject = null;
            
            Debug.DrawRay(this.transform.position, this.transform.forward * _maxDistance, Color.red);
        }

        public bool CheckRaycast(GameObject target)
        {
            if (TargetedObject == null) return false;
            return TargetedObject == target;
        }

        public bool CheckRaycast(string tag)
        {
            if (TargetedObject == null) return false;
            return TargetedObject.CompareTag(tag);
        }
    }
}