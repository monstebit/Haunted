namespace UU
{
    public class IdlingState : MovementState
    {
        public IdlingState(
            IStateSwitcher stateSwitcher, 
            PlayerInputManager playerInputManager, 
            StateMachineData data) : base(stateSwitcher, playerInputManager, data)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            PlayerView.StartIdling();
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerView.StopIdling();
        }

        public override void Update()
        {
            base.Update();

            if (IsPlayerWalking())
            {
                StateSwitcher.SwitchState<WalkingState>();
            }
            else if (IsPlayerSprinting())
            {
                StateSwitcher.SwitchState<SprintingState>();
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }
    }
}