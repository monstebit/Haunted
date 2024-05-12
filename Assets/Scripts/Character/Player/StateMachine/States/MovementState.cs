using UnityEngine;

namespace UU
{
    public abstract class MovementState : IState
    {
        //  TEMP
            //  CAMERA
        private float _leftAndRightLookAngle;
        private float _upAndDownLookAngle;
        private float _leftAndRightRotationSpeed = 220;
        private float _upAndDownRotationSpeed = 220;
        // private float _minimumPivot = 330;  //  Определяет нижнюю границу поворота камеры.
        private float _minimumPivot = -30;  //  Определяет нижнюю границу поворота камеры.
        private float _maximumPivot = 999;   //  Определяет верхнюю границу поворота камеры.
        // private Transform _cameraPivotTransform;
        private float _cameraSmoothSpeed = 1;
        // private Vector3 _cameraVelocity;
        private Vector3 _cameraVelocity = Vector3.zero;
        private Vector3 _cameraObjectPosition;
        private float _cameraZPosition;
        private float _targetCameraZPosition;
        private float _cameraObjectPositionZInterpolation = 0.2f;
            //  MOVEMENT
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        
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
            // if (IsMovementInputZero())
                // return;
            
            //  ВЫВОД ТИПА НАСЛЕДНИКА (В КАКОМ STATE МЫ СЕЙЧАС НАХОДИМСЯ)
            // Debug.Log(GetType());
        }
        
        public virtual void Exit() { }

        public virtual void HandleInput()
        {
            Data.MovementInput = ReadMovementInput();
            // Debug.Log($"MOVEMENT X = {Data.MovementInput.x}, y = {Data.MovementInput.y}");
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
            // Debug.Log($"CAMERA X = {Data.CameraInput.x}, y = {Data.CameraInput.y}");
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

        protected bool IsMovementInputZero() => Data.MovementInput == Vector2.zero;
        protected bool IsCameraMovementInputZero() => Data.CameraInput == Vector2.zero;
        protected bool IsPlayerWalking() => Data.MoveAmount > 0 && Data.MoveAmount <= 0.5f;
        protected bool IsPlayerSprinting() => Data.MoveAmount > 0.5f;
        
        private Vector2 ReadMovementInput() => PlayerControls.PlayerMovement.Movement.ReadValue<Vector2>();
        private Vector2 ReadCameraInput() => PlayerControls.PlayerMovement.Camera.ReadValue<Vector2>();
        
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
            // _leftAndRightLookAngle += Data.CameraHorizontalInput * _leftAndRightRotationSpeed;
            // _upAndDownLookAngle += (Data.CameraVerticalInput * _upAndDownLookAngle) * Time.deltaTime; 
            // _upAndDownLookAngle = Mathf.Clamp(
            //     _upAndDownLookAngle, 
            //     _minimumPivot, 
            //     _maximumPivot);
            //
            // Vector3 cameraRotation = Vector3.zero;
            // Quaternion targetRotation;
            //
            // cameraRotation.y = _leftAndRightLookAngle;
            // targetRotation = Quaternion.Euler(cameraRotation);
            // PlayerCamera.instance.transform.rotation = targetRotation;
            //
            // cameraRotation = Vector3.zero;
            // cameraRotation.x = _upAndDownLookAngle;
            // targetRotation = Quaternion.Euler(cameraRotation);
            // PlayerCamera.instance._cameraPivotTransform.localRotation = targetRotation;

            // float _leftAndRightLookAngle;
            // float _upAndDownLookAngle;
            
            PlayerCamera.instance.leftAndRightLookAngle = _leftAndRightLookAngle;
            Debug.Log($"UP DOWN = {_upAndDownLookAngle}");
            PlayerCamera.instance.upAndDownLookAngle = _upAndDownLookAngle;
            
            //  =================================
            Vector3 currentCameraRotation = PlayerCamera.instance.transform.eulerAngles;
            float xRotation = currentCameraRotation.x;
            float yRotation = currentCameraRotation.y;
            float zRotation = currentCameraRotation.z;
            
            Vector3 pivotRotation = PlayerCamera.instance._cameraPivotTransform.transform.eulerAngles;
            float xRotationPivot = pivotRotation.x;
            float yRotationPivot = pivotRotation.y;
            float zRotationPivot = pivotRotation.z;
            //  =================================
            
            // _leftAndRightLookAngle = PlayerCamera.instance.transform.eulerAngles.y;
            _leftAndRightLookAngle = yRotation;
            // _upAndDownLookAngle = PlayerCamera.instance._cameraPivotTransform.localEulerAngles.x;
            _upAndDownLookAngle = xRotationPivot;
            
            _leftAndRightLookAngle += (Data.CameraHorizontalInput * _leftAndRightRotationSpeed) * Time.deltaTime;
            _upAndDownLookAngle -= (Data.CameraVerticalInput * _upAndDownRotationSpeed) * Time.deltaTime; // Вычисляем новый угол поворота камеры
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);   // Ограничиваем угол между минимальным и максимальным значением

            //  ПОВОРОТ КАМЕРЫ ОТНОСИТЕЛЬНО ИГРОКА ВОКРУГ ОСИ Y
            PlayerCamera.instance.transform.RotateAround(_playerInputManager.transform.position, Vector3.up, Data.CameraHorizontalInput);
            
            // ПОВОРОТ КАМЕРЫ ОТНОСИТЕЛЬНО ИГРОКА ВОКРУГ ОСИ X (ДЁРГАЕТ КАМЕРУ)
            PlayerCamera.instance._cameraPivotTransform.localEulerAngles = new Vector3(_upAndDownLookAngle, 0f, 0f);    // Вращаем камеру вокруг оси X локально

            // Vector3 cameraRotation = Vector3.zero;
            // Quaternion targetRotation;
            // //  ROTATE THIS PIVOT GAMEOBJECT UP AND DOWN 
            // cameraRotation = Vector3.zero;
            // cameraRotation.x = _upAndDownLookAngle;
            // targetRotation = Quaternion.Euler(cameraRotation);
            // PlayerCamera.instance._cameraPivotTransform.localRotation = targetRotation;
        }
        
        //  PLAYER MOVEMENT
        public void HandleAllMovement()
        {
            //  ДОБАВИЛ ПРОВЕРКУ НА INPUT
            if (IsMovementInputZero())
                return;
            
            HandleGroundedMovement();
            HandleRotation();
        }
        
        private void HandleGroundedMovement()
        {
            // _moveDirection = PlayerCamera.instance.transform.forward * Data.MovementInput.y;
            // _moveDirection = _moveDirection + PlayerCamera.instance.transform.right * Data.VerticalInput;
            // _moveDirection.Normalize();
            // _moveDirection.y = 0;
            //
            // if (Data.MoveAmount > 0.5f)
            // {
            //     _playerInputManager.CharacterController.Move(_moveDirection * Data.Speed * Time.deltaTime);
            // }
            // else if (Data.MoveAmount >= 0.5f)
            // {
            //     _playerInputManager.CharacterController.Move(_moveDirection * Data.Speed * Time.deltaTime);
            // }
            
            _moveDirection = new Vector3(Data.MovementInput.x,0 ,Data.MovementInput.y);
            _playerInputManager.CharacterController.Move(_moveDirection * Data.Speed * Time.deltaTime);
        }
        
        private void HandleRotation()
        {
            // _targetRotationDirection = Vector3.zero;
            // _targetRotationDirection = PlayerCamera.instance.CameraObject.transform.forward * Data.VerticalInput;
            // _targetRotationDirection = _targetRotationDirection + PlayerCamera.instance.CameraObject.transform.right * Data.HorizontalInput;
            // _targetRotationDirection.Normalize();
            // _targetRotationDirection.y = 0;
            //
            // if (_targetRotationDirection == Vector3.zero)
            // {
            //     _targetRotationDirection = _playerInputManager.transform.forward;
            // }
            //
            // Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            // Quaternion targetRotation = Quaternion.Slerp(
            //     _playerInputManager.transform.rotation, 
            //     newRotation, 
            //     Data.RotationSpeed * Time.deltaTime);
            //
            // _playerInputManager.transform.rotation = targetRotation;

            // Quaternion toRotation = Quaternion.LookRotation(PlayerCamera.instance.transform.position);
        }
    }
}