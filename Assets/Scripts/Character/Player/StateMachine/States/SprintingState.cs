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
            
            PlayerView.StartSprinting();
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerView.StopSprinting();
        }

        public override void Update()
        {
            base.Update();
        
            if (IsMovementInputZero())
                StateSwitcher.SwitchState<WalkingState>();
        }
    }
}