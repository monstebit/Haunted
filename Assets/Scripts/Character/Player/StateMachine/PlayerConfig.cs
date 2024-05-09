using UnityEngine;

namespace UU
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private WalkingStateConfig _walkingStateConfig;
        [SerializeField] private SprintingStateConfig _sprintingStateConfig;

        public WalkingStateConfig WalkingStateConfig => _walkingStateConfig;
        public SprintingStateConfig SprintingStateConfig => _sprintingStateConfig;
    }
}