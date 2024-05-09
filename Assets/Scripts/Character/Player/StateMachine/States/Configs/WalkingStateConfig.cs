using System;
using UnityEngine;

namespace UU
{
    [Serializable]
    public class WalkingStateConfig
    {
        [SerializeField, Range(0, 10)] private float _walkingSpeed;

        public float WalkingSpeed => _walkingSpeed;
    }
}