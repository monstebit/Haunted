using System;
using UnityEngine;

namespace UU
{
    public class StateMachineData : MonoBehaviour
    {
        //  MOVEMENT
        public float RotationSpeed = 5f;
        
        public Vector2 MovementInput;
        private float _verticalInput;
        private float _horizontalInput;
        
        //  CAMERA
        public Vector2 CameraInput;
        private float _cameraVerticalInput;
        private float _cameraHorizontalInput;
        
        private float _moveAmount;
        private float _speed;
        
        //  MOVEMENT
        public float VerticalInput
        {
            get => _verticalInput;
            set
            {
                // if (value < -1 || value > 1)
                //     throw new ArgumentOutOfRangeException(nameof(value));
        
                _verticalInput = value;
            }
        }
        
        public float HorizontalInput
        {
            get => _horizontalInput;
            set
            {
                // if (value < -1 || value > 1)
                //     throw new ArgumentOutOfRangeException(nameof(value));
        
                _horizontalInput = value;
            }
        }
        
        //  CAMERA
        public float CameraVerticalInput
        {
            get => _cameraVerticalInput;
            set
            {
                // if (value < -1 || value > 1)
                //     throw new ArgumentOutOfRangeException(nameof(value));
        
                _cameraVerticalInput = value;
            }
        }
        
        public float CameraHorizontalInput
        {
            get => _cameraHorizontalInput;
            set
            {
                // if (value < -1 || value > 1)
                //     throw new ArgumentOutOfRangeException(nameof(value));
        
                _cameraHorizontalInput = value;
            }
        }
        
        //  ПАРАМЕТРЫ
        public float MoveAmount
        {
            get => _moveAmount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _moveAmount = value;
            }
        }
        
        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _speed = value;
            }
        }
    }
}