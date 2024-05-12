using System;
using UnityEngine;

namespace UU
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerView _playerView;
        
        private PlayerControls _playerControls;
        private PlayerStateMachine _playerStateMachine;
        private CharacterController _characterController;
        
        public PlayerControls PlayerControls => _playerControls;
        public CharacterController CharacterController => _characterController;
        public PlayerConfig PlayerConfig => _playerConfig;
        public PlayerView PlayerView => _playerView;
        
        private void Awake()
        {
            PlayerView.Initialize();
            
            _characterController = GetComponent<CharacterController>();
            
            _playerControls = new PlayerControls();
            // _playerControls.PlayerMovement.Movement.performed += i => PlayerSt.movementInput = i.ReadValue<Vector2>();
            // _playerControls.PlayerMovement.Camera.performed += i => cameratInput = i.ReadValue<Vector2>();
            
            _playerStateMachine = new PlayerStateMachine(this);
        }

        private void Update()
        {
            _playerStateMachine.HandleInput();
            _playerStateMachine.Update();
        }

        private void LateUpdate()
        {
            _playerStateMachine.LateUpdate();
        }

        private void OnEnable() => _playerControls.Enable();
        
        private void OnDisable() => _playerControls.Disable();
    }
}