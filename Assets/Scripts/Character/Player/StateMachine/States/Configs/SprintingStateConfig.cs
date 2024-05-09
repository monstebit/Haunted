using System;
using UnityEngine;

namespace UU
{
    [Serializable]
    public class SprintingStateConfig
    {
        [SerializeField, Range(0, 10)] private float _sprintingSpeed;

        public float SprintingSpeed => _sprintingSpeed;
    }
}