namespace UU
{
    public class SprintingState : MovementState
    {
        private SprintingStateConfig _sprintingStateConfig;

        public SprintingState(
            IStateSwitcher stateSwitcher,
            PlayerInputManager playerInputManager,
            StateMachineData data) : base(stateSwitcher, playerInputManager, data)
            => _sprintingStateConfig = playerInputManager.PlayerConfig.SprintingStateConfig;

        public override void Enter()
        {
            base.Enter();

            Data.Speed = _sprintingStateConfig.SprintingSpeed;
        }

        public override void Update()
        {
            base.Update();
        
            if (IsMovementInputZero())
                StateSwitcher.SwitchState<WalkingState>();
        }
    }
}