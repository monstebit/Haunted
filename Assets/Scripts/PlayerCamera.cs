using UnityEngine;

namespace UU
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        
        public Camera CameraObject;
        
        [SerializeField] public Transform _cameraPivotTransform;
        public float minimumPivot = -30;
        public float maximumPivot = 60;
        public float leftAndRightRotationSpeed = 1;
        public float upAndDownLookAngle;
        public float leftAndRightLookAngle;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}