using UnityEngine;

namespace UU
{
    public abstract class MovementState : IState
    {
        //  CAMERA
        private float _leftAndRightLookAngle;
        private float _upAndDownLookAngle;
        private float _leftAndRightRotationSpeed = 220;
        private float _upAndDownRotationSpeed = 220;
        private float _minimumPivot = -30;  //  Определяет нижнюю границу поворота камеры.
        private float _maximumPivot = 90;   //  Определяет верхнюю границу поворота камеры.
        private float _cameraSmoothSpeed = 1;
        private Vector3 _cameraVelocity = Vector3.zero;
        private Vector3 _cameraObjectPosition;
        private float _cameraZPosition;
        private float _targetCameraZPosition;
        private float _cameraObjectPositionZInterpolation = 0.2f;
        //  MOVEMENT
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        private float _rotationSpeed = 15;
        
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
        protected PlayerView PlayerView => _playerInputManager.PlayerView;

        public virtual void Enter()
        {
            //  ВЫВОД ТИПА НАСЛЕДНИКА (В КАКОМ STATE МЫ СЕЙЧАС НАХОДИМСЯ)
            // Debug.Log(GetType());
            
            _leftAndRightLookAngle = Data.SavedLeftAndRightLookAngle;
            _upAndDownLookAngle = Data.SavedUpAndDownLookAngle;
        }

        public virtual void Exit()
        {
            Data.SavedLeftAndRightLookAngle = _leftAndRightLookAngle;
            Data.SavedUpAndDownLookAngle = _upAndDownLookAngle;
        }

        public virtual void HandleInput()
        {
            Data.MovementInput = ReadMovementInput();
            Data.VerticalInput = Data.MovementInput.y;
            Data.HorizontalInput = Data.MovementInput.x;
    
            Data.MoveAmount = Mathf.Clamp01(Mathf.Abs(Data.VerticalInput) + Mathf.Abs(Data.HorizontalInput));
    
            if (Data.MoveAmount <= 0.5 && Data.MoveAmount > 0)
            {
                Data.MoveAmount = 0.5f;
            }
            else if (Data.MoveAmount > 0.5 && Data.MoveAmount <= 1)
            {
                Data.MoveAmount = 1;
            }
            
            Data.CameraInput = ReadCameraInput();
            Data.CameraVerticalInput = Data.CameraInput.y;
            Data.CameraHorizontalInput = Data.CameraInput.x;
        }

        public virtual void Update()
        {
            HandleAllMovement();
        }

        public virtual void LateUpdate()
        {
            HandleAllCameraActions();
        }
        
        protected bool IsPlayerWalking() => Data.MoveAmount > 0 && Data.MoveAmount <= 0.5f;
        
        protected bool IsPlayerSprinting() => Data.MoveAmount > 0.5f;
        
        private Vector2 ReadMovementInput() => PlayerControls.PlayerMovement.Movement.ReadValue<Vector2>();
        
        private Vector2 ReadCameraInput() => PlayerControls.PlayerCamera.Movement.ReadValue<Vector2>();
        
        //  КАМЕРА
        public void HandleAllCameraActions()
        {
            HandleFollowTarget();
            HandleCameraRotation();
        }

        private void HandleFollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(
                PlayerCamera.instance.transform.position,
                _playerInputManager.transform.position,
                ref _cameraVelocity,
                _cameraSmoothSpeed * Time.deltaTime);
            
            PlayerCamera.instance.transform.position = targetPosition;
        }
        
        private void HandleCameraRotation()
        {
            // Обновляем углы поворота в соответствии с вводом
            _leftAndRightLookAngle += (Data.CameraHorizontalInput * _leftAndRightRotationSpeed) * Time.deltaTime;
            _upAndDownLookAngle -= (Data.CameraVerticalInput * _upAndDownRotationSpeed) * Time.deltaTime;
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);
            
            // Поворачиваем камеру вокруг игрока по оси Y
            Quaternion targetRotation = Quaternion.Euler(0f, _leftAndRightLookAngle, 0f);
            PlayerCamera.instance.transform.localRotation = targetRotation;

            // Поворачиваем пивот камеры вокруг игрока по оси X локально
            Quaternion pivotRotation = Quaternion.Euler(_upAndDownLookAngle, 0f, 0f);
            PlayerCamera.instance._cameraPivotTransform.localRotation = pivotRotation;
        }

        //  PLAYER MOVEMENT
        public void HandleAllMovement()
        {
            //  ДОБАВИЛ ПРОВЕРКУ НА INPUT
            // if (IsMovementInputZero())
            //     return;
            
            HandleGroundedMovement();
            HandleRotation();
        }
        
        private void HandleGroundedMovement()
        {
            // Получаем направление камеры
            Vector3 forward = PlayerCamera.instance.transform.forward;
            Vector3 right = PlayerCamera.instance.transform.right;
            // Рассчитываем направление движения на основе камеры
            _moveDirection = forward * Data.VerticalInput + right * Data.HorizontalInput;
            // Нормализация вектора направления
            _moveDirection.Normalize();
            // Убираем влияние высоты
            _moveDirection.y = 0;
            // Перемещение персонажа
            _playerInputManager.CharacterController.Move(_moveDirection * Data.Speed * Time.deltaTime);
        }
        
        private void HandleRotation()
        {
            Transform cameraObjectTransform = PlayerCamera.instance.CameraObject.transform;

            Vector3 cameraObjectForward = cameraObjectTransform.forward;
            Vector3 cameraObjectRight = cameraObjectTransform.right;

            _targetRotationDirection = cameraObjectForward * Data.VerticalInput;
            _targetRotationDirection = _targetRotationDirection + cameraObjectRight * Data.HorizontalInput;
            _targetRotationDirection.y = 0; // Убираем влияние высоты
            _targetRotationDirection.Normalize(); // Нормализуем вектор

            // Проверка на ненулевое направление
            if (_targetRotationDirection != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
                
                Quaternion targetRotation = Quaternion.Slerp(
                    PlayerView.transform.rotation,
                    newRotation,
                    _rotationSpeed * Time.deltaTime);
                
                PlayerView.transform.rotation = targetRotation;
            }
        }
    }
}