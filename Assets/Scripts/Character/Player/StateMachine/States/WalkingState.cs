namespace UU
{
    public class WalkingState : MovementState
    {
        private WalkingStateConfig _walkingStateConfig;

        public WalkingState(
            IStateSwitcher stateSwitcher, 
            PlayerInputManager playerInputManager, 
            StateMachineData data) : base(stateSwitcher, playerInputManager, data)
            => _walkingStateConfig = playerInputManager.PlayerConfig.WalkingStateConfig;
        
        public override void Enter()
        {
            base.Enter();
            
            Data.Speed = _walkingStateConfig.WalkingSpeed;
            
            PlayerView.StartWalking();
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerView.StopWalking();
        }

        public override void Update()
        {
            base.Update();
        
            if (IsMovementInputZero())
                StateSwitcher.SwitchState<IdlingState>();
            else if (IsPlayerSprinting())
                StateSwitcher.SwitchState<SprintingState>();
        }
    }
}