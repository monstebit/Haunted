using System;
using UnityEngine;

namespace UU
{
    public class StateMachineData : MonoBehaviour
    {
        public float verticalMovement;
        public float horizontalMovement;
        
        //  PLAYER CAMERA
        public Vector2 CameratInput;
        private float CameraHorizontalInput;    //  CONFIG?
        private float CameraVerticalInput;      //  CONFIG?
        
        //  PLAYER MOVEMENT
        public Vector2 MovementInput;
        public float RotationSpeed = 5f;        //  CONFIG?
        
        private float _moveAmount;
        private float _speed;
        
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