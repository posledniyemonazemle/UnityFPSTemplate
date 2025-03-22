using UnityEngine;
using Game.Player;
using Game.Interaction;

namespace Game.Core
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private PlayerCrouch _playerCrouch;
        [SerializeField] private PlayerSprint _playerSprint;
        [SerializeField] private CameraSway _cameraSway;
        [SerializeField] private PlayerJump _playerJump;
        [SerializeField] private DraggableObjects _draggableObjects;

        private void Awake()
        {
            _playerMove?.Initialize();
            _playerLook?.Initialize();
            _playerCrouch?.Initialize();
            _playerSprint?.Initialize();
            _playerJump?.Initialize();
            _cameraSway?.Initialize();
            _draggableObjects?.Initialize();
        }
    }
}