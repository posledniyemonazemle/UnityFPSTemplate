using UnityEngine;

namespace Game.Player
{
    public class CameraSway : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _bobbingCurve;
        [SerializeField] private float _bobbingDuration = 1f;
        [SerializeField] private float _bobbingAmount = 0.1f;
        [SerializeField] private float _returnSpeed = 1.0f;

        private PlayerMove _playerMove;
        private GroundCheck _groundCheck;
        private const string PlayerTag = "Player";

        private Vector3 _originalPosition;
        private float _bobbingTimer = 0f;
        private float _curveTime;

        public void Initialize()
        {
            _originalPosition = transform.localPosition;
            _playerMove = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<PlayerMove>();
            _groundCheck = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<GroundCheck>();
        }

        private void Update()
        {
            if (_playerMove.RB.linearVelocity.magnitude > 0.1f && _groundCheck.IsGrounded)
            {
                HandleBobbingTimmer();
                HandleCameraPos();
            }
            else StopBobbing();
        }

        private void Reset()
        {
            _bobbingTimer = 0f;
            _curveTime = 0f;
        }

        private void HandleBobbingTimmer()
        {
            _bobbingTimer += Time.deltaTime;
            if (_bobbingTimer > _bobbingDuration) _bobbingTimer = 0f;
            _curveTime = _bobbingTimer / _bobbingDuration;
        }
        
        private void HandleCameraPos()
        {
            float curveVal = _bobbingCurve.Evaluate(_curveTime);
            this.transform.localPosition = _originalPosition + Vector3.up * curveVal * _bobbingAmount;
        }

        private void StopBobbing()
        {
            Reset();
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _originalPosition,
                _returnSpeed * Time.deltaTime);
        }
    }
}