using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UU
{
    public class StateMachineData : MonoBehaviour
    {
        public float verticalMovement;
        public float horizontalMovement;

        //  TEMP
        public Vector2 CameratInput;
        public float CameraHorizontalInput;
        public float CameraVerticalInput;
        
        //  UU
        public Vector2 MovementInput;
        public float RotationSpeed = 5f;
        
        private float _moveAmount;
        private float _walkingSpeed;
        private float _runningSpeed ;
        
        private float _verticalInput;
        private float _horizontalInput;
        
        public float VerticalInput
        {
            get => _verticalInput;
            set
            {
                if (value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _verticalInput = value;
            }
        }
        
        public float HorizontalInput
        {
            get => _horizontalInput;
            set
            {
                if (value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _horizontalInput = value;
            }
        }
        
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
        
        public float RunningSpeed
        {
            get => _runningSpeed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _runningSpeed = value;
            }
        }
        
        public float WalkingSpeed
        {
            get => _walkingSpeed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
        
                _walkingSpeed = value;
            }
        }
    }
}