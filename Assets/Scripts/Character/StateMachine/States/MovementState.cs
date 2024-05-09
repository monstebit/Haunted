using UnityEngine;

namespace UU
{
    public abstract class MovementState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly StateMachineData Data;
        
        private readonly PlayerInputManager _playerInputManager;
        
        public MovementState(IStateSwitcher stateSwitcher, PlayerInputManager playerInputManager, StateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            _playerInputManager = playerInputManager;
            Data = data;
        }
        
        protected PlayerControls PlayerControls => _playerInputManager.PlayerControls;
        protected CharacterController CharacterController => _playerInputManager.CharacterController;
        
        public virtual void Enter()
        {
            // Debug.Log(GetType());
        }

        public virtual void Exit() { }

        public virtual void HandleInput()
        {
            Data.MovementInput = ReadInput();
            
            Data.MoveAmount = Mathf.Clamp01(Mathf.Abs(Data.VerticalInput) + Mathf.Abs(Data.HorizontalInput));

            if (Data.MoveAmount <= 0.5 && Data.MoveAmount > 0)
            {
                Data.MoveAmount = 0.5f;
            }
            else if (Data.MoveAmount > 0.5 && Data.MoveAmount <= 1)
            {
                Data.MoveAmount = 1;
            }
            
            Data.HorizontalInput = Data.MovementInput.x;
            Data.horizontalMovement = Data.HorizontalInput;
            
            Data.VerticalInput = Data.MovementInput.y;
            Data.verticalMovement = Data.VerticalInput;
        }

        public virtual void Update()
        {
            Vector3 velocity = GetConvertedVelocity();
            
            if (Data.MoveAmount > 0.5f)
            {
                CharacterController.Move(velocity * Data.RunningSpeed * Time.deltaTime);
            }
            else if (Data.MoveAmount <= 0.5f)
            {
                CharacterController.Move(velocity * Data.WalkingSpeed * Time.deltaTime);
            }
            
            _playerInputManager.transform.rotation = GetRotationFrom(velocity);
        }

        protected bool IsHorizontalInputZero() => Data.MovementInput.x == 0;
        
        protected bool IsVerticalInputZero() => Data.MovementInput.y == 0;

        private Quaternion GetRotationFrom(Vector3 velocity)
        {
            if (velocity == Vector3.zero)
            {
                return _playerInputManager.transform.rotation;
            }

            return GetTargetRotation();
        }
        
        //  UU
        private Quaternion GetTargetRotation()
        {
            // Создаем новый поворот в нужном направлении
            Quaternion newRotation = Quaternion.LookRotation(GetConvertedVelocity());
            
            // Интерполируем между текущим поворотом и новым
            Quaternion targetRotation = Quaternion.Slerp(_playerInputManager.transform.rotation, newRotation, Data.RotationSpeed * Time.deltaTime);

            return targetRotation;
        }

        private Vector3 GetConvertedVelocity() => new Vector3(Data.horizontalMovement, 0, Data.verticalMovement);
        
        private Vector2 ReadInput() => PlayerControls.PlayerMovement.Movement.ReadValue<Vector2>();


    }
}