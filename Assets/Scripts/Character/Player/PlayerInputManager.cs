using UnityEngine;

namespace UU
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInputManager : MonoBehaviour //  PlayerInputManager ПЕРЕИМЕНОВАТЬ?
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerView _playerView;
        
        //  PLAYER CAMERA
        private Camera _playerCamera;
        
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
            _playerStateMachine = new PlayerStateMachine(this);
        }

        private void Update()
        {
            _playerStateMachine.HandleInput();
            _playerStateMachine.Update();
        }

        private void OnEnable() => _playerControls.Enable();
        
        private void OnDisable() => _playerControls.Disable();
    }
}