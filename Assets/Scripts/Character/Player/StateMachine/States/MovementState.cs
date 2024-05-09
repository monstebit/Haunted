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
            //  ВЫВОД ТИПА НАСЛЕДНИКА (В КАКОМ STATE МЫ СЕЙЧАС НАХОДИМСЯ)
            Debug.Log(GetType());
            Debug.Log(Data.Speed);
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
            Vector3 moveDirection = GetMovementDirection();
            moveDirection.Normalize();
            
            CharacterController.Move(moveDirection * Data.Speed * Time.deltaTime);
            
            _playerInputManager.transform.rotation = GetRotationFrom(moveDirection);
        }

        protected bool IsMovementInputZero() => Data.MovementInput == Vector2.zero;
        // protected bool IsPlayerWalking() => Data.MoveAmount > 0 && Data.MoveAmount <= 0.5f;
        protected bool IsPlayerSprinting() => Data.MoveAmount > 0.5f;
        
        private Vector2 ReadInput() => PlayerControls.PlayerMovement.Movement.ReadValue<Vector2>();
        
        //  КОРРЕКТНО ЛИ y = 0?
        private Vector3 GetMovementDirection() => new Vector3(Data.horizontalMovement, 0, Data.verticalMovement);
        
        private Quaternion GetRotationFrom(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return _playerInputManager.transform.rotation;
            }

            return GetTargetRotation();
        }
        
        //  UU
        private Quaternion GetTargetRotation()
        {
            // Создаем новый поворот в нужном направлении
            Quaternion newRotation = Quaternion.LookRotation(GetMovementDirection());
            
            // Интерполируем между текущим поворотом и новым
            Quaternion targetRotation = Quaternion.Slerp(
                _playerInputManager.transform.rotation, newRotation, Data.RotationSpeed * Time.deltaTime);

            return targetRotation;
        }
    }
}