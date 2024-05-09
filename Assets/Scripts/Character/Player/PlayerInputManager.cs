using UnityEngine;

namespace UU
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInputManager : MonoBehaviour //  PlayerInputManager ПЕРЕИМЕНОВАТЬ?
    {
        //  TEMP
        [Header("CAMERA MOVEMENT INPUT")]
        private Camera _playerCamera;
        
        private PlayerControls _playerControls;
        private CharacterStateMachine _characterStateMachine;
        private CharacterController _characterController;

        public PlayerControls PlayerControls => _playerControls;
        public CharacterController CharacterController => _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            _playerControls = new PlayerControls();
            _characterStateMachine = new CharacterStateMachine(this);
        }

        private void Update()
        {
            _characterStateMachine.HandleInput();
            _characterStateMachine.Update();
        }

        private void OnEnable() => _playerControls.Enable();
        
        private void OnDisable() => _playerControls.Disable();
    }
}